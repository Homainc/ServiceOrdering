import { history, api } from "../../_helpers";
import { ThunkAction } from "redux-thunk";
import { UserAuthDto, UserCreateDto, EmployeeProfileDto, ValidationProblemDetails, AccessTokenDto, ProblemDetails } from "../../WebApiModels";
import { AuthActionTypes, 
    AUTH_LOGIN_REQUEST, AUTH_LOGIN_SUCCESS, AUTH_LOGIN_FAILURE, 
    AUTH_LOGOUT, 
    AUTH_SIGN_UP_REQUEST, AUTH_SIGN_UP_SUCCESS, AUTH_SIGN_UP_FAILURE, 
    AUTH_UPDATE_USER, AUTH_UPDATE_EMPLOYEE, AUTH_UPDATE_TOKEN, AUTH_REFRESHING_TOKEN, AUTH_DONE_REFRESHING_TOKEN 
} from "./types";
import { AlertActionTypes } from "../alert/types";
import * as alertActions from '../alert/actions';
import { RootState } from "..";
import { HubConnectionBuilder } from '@aspnet/signalr';

export function logIn(
    email: string, 
    password: string
): ThunkAction<void, RootState, undefined, AuthActionTypes | AlertActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            const user = (await api.Account_Auth({ loginModel: { userEmail: email, userPassword: password } })).body as UserAuthDto;

            localStorage.setItem('user', JSON.stringify(user));
            api.setRequestHeadersHandler(h => ({ ...h, 'Authorization': 'Bearer ' + user.token?.token }));
            
            dispatch(success(user));
            dispatch(alertActions.success(`You have successfully logged in as ${user.email}`));
            dispatch(connectToNotificationHub());
            
            history.push('/');
            
            return user;
        }
        catch (err) {
            const errObj = err.response.body as ValidationProblemDetails;
            dispatch(alertActions.error(errObj.detail || ''));
            dispatch(failure(errObj));
            throw errObj.errors;
        }
    };

    function request(): AuthActionTypes { 
        return { type: AUTH_LOGIN_REQUEST }; 
    }
    function success(user: UserAuthDto): AuthActionTypes { 
        return { type: AUTH_LOGIN_SUCCESS, user }; 
    }
    function failure(error: ValidationProblemDetails): AuthActionTypes { 
        return { type: AUTH_LOGIN_FAILURE, error }; 
    }
}

export function logOut(): AuthActionTypes {
    localStorage.removeItem('user');
    api.setRequestHeadersHandler(h => h);

    return { type: AUTH_LOGOUT };
}

export function signUp(
    user: UserCreateDto
): ThunkAction<void, RootState, undefined, AuthActionTypes | AlertActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            const authUser = (await api.Account_SignUp({ userDto: user })).body as UserAuthDto;

            localStorage.setItem('user', JSON.stringify(user));
            api.setRequestHeadersHandler(h => ({ ...h, 'Authorization': 'Bearer ' + authUser.token?.token }));

            dispatch(success(authUser));
            dispatch(alertActions.success('You have successfully signed up!'));
            dispatch(connectToNotificationHub());

            history.push('/');

            return user;
        }
        catch (err){
            const errObj = err.response.body as ValidationProblemDetails;
            dispatch(alertActions.error(errObj.title || ''));
            dispatch(failure(errObj));
            throw errObj.errors;
        }
    };

    function request(): AuthActionTypes { 
        return { type: AUTH_SIGN_UP_REQUEST }; 
    }
    function success(user: UserAuthDto): AuthActionTypes { 
        return { type: AUTH_SIGN_UP_SUCCESS, user }; 
    }
    function failure(error: ValidationProblemDetails): AuthActionTypes { 
        return { type: AUTH_SIGN_UP_FAILURE, error };
    }
}

export function updateUser(user: UserAuthDto): AuthActionTypes {
    const oldUser = JSON.parse(localStorage.getItem('user') as string);
    user.token = oldUser.token;
    localStorage.setItem('user', JSON.stringify(user));

    return { type: AUTH_UPDATE_USER, user };
}

export function updateEmployee(employeeProfile?: EmployeeProfileDto): AuthActionTypes {
    const user = JSON.parse(localStorage.getItem('user') as string);
    user.employeeProfile = employeeProfile;
    localStorage.setItem('user', JSON.stringify(user));

    return { type: AUTH_UPDATE_EMPLOYEE, employee: employeeProfile };
}

export function connectToNotificationHub(
): ThunkAction<void, RootState, undefined, AuthActionTypes> {
    return async (dispatch, getState) => {
        const userToken = getState().auth.user?.token;
        try {
            const hubConnection = new HubConnectionBuilder()
            .withUrl('/notification', { accessTokenFactory: () => userToken?.token || '' })
            .build();
      
            await hubConnection.start();

            hubConnection.on('ReceiveNotice', msg => dispatch(alertActions.info(msg)));
        }
        catch (err) {
            dispatch(logOut());
        }
    };
}

export function updateToken(token: AccessTokenDto): AuthActionTypes {
    const user = JSON.parse(localStorage.getItem('user') as string);
    user.token = token;
    localStorage.setItem('user', JSON.stringify(user));

    return { type: AUTH_UPDATE_TOKEN, token: token };
}

export const refreshToken = (dispatch: any, token: AccessTokenDto): any => {
    var refreshTokenPromise = api.Account_RefreshToken({ accessToken: token as AccessTokenDto })
        .then(resp => {
            const newToken = resp.body as AccessTokenDto;
            dispatch({ type: AUTH_DONE_REFRESHING_TOKEN });
            dispatch(updateToken(newToken));
            return Promise.resolve(newToken);
        })
        .catch(err => {
            dispatch({ type: AUTH_DONE_REFRESHING_TOKEN });
            console.log('error refreshing token', err);
            dispatch(logOut());
            return Promise.reject(err);
        });

    dispatch({
        type: AUTH_REFRESHING_TOKEN,
        // we want to keep track of token promise in the state so that we don't try to refresh
        // the token again while refreshing is in process
        refreshingPromise: refreshTokenPromise
    });

    return refreshTokenPromise;
};