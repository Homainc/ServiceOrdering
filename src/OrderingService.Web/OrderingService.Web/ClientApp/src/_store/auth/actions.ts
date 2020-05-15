import { history, api, getErrorMessageFromEx } from "../../_helpers";
import { ThunkAction } from "redux-thunk";
import { EmployeeProfileDTO, UserAuthDto, UserCreateDto } from "../../WebApiModels";
import { AuthActionTypes, 
    AUTH_LOGIN_REQUEST, AUTH_LOGIN_SUCCESS, AUTH_LOGIN_FAILURE, 
    AUTH_LOGOUT, 
    AUTH_SIGN_UP_REQUEST, AUTH_SIGN_UP_SUCCESS, AUTH_SIGN_UP_FAILURE, 
    AUTH_UPDATE_USER, AUTH_UPDATE_EMPLOYEE 
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
            api.setRequestHeadersHandler(h => ({ ...h, 'Authorization': 'Bearer ' + user.token }));
            
            dispatch(success(user));
            dispatch(alertActions.success(`You have successfully logged in as ${user.email}`));
            dispatch(connectToNotificationHub());
            
            history.push('/');
            
            return user;
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(alertActions.error(errorMsg));
            dispatch(failure(errorMsg));
        }
    };

    function request(): AuthActionTypes { 
        return { type: AUTH_LOGIN_REQUEST }; 
    }
    function success(user: UserAuthDto): AuthActionTypes { 
        return { type: AUTH_LOGIN_SUCCESS, user }; 
    }
    function failure(error: string): AuthActionTypes { 
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
            api.setRequestHeadersHandler(h => ({ ...h, 'Authorization': 'Bearer ' + authUser.token }));

            dispatch(success(authUser));
            dispatch(alertActions.success('You have successfully signed up!'));
            dispatch(connectToNotificationHub());

            history.push('/');

            return user;
        }
        catch (err){
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(alertActions.error(errorMsg));
            dispatch(failure(errorMsg));
        }
    };

    function request(): AuthActionTypes { 
        return { type: AUTH_SIGN_UP_REQUEST }; 
    }
    function success(user: UserAuthDto): AuthActionTypes { 
        return { type: AUTH_SIGN_UP_SUCCESS, user }; 
    }
    function failure(error: string): AuthActionTypes { 
        return { type: AUTH_SIGN_UP_FAILURE, error };
    }
}

export function updateUser(user: UserAuthDto): AuthActionTypes {
    const oldUser = JSON.parse(localStorage.getItem('user') as string);
    user.token = oldUser.token;
    localStorage.setItem('user', JSON.stringify(user));

    return { type: AUTH_UPDATE_USER, user };
}

export function updateEmployee(employeeProfile: EmployeeProfileDTO | undefined): AuthActionTypes {
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
            .withUrl('/notification', { accessTokenFactory: () => userToken || '' })
            .build();
      
            await hubConnection.start();

            hubConnection.on('ReceiveNotice', msg => dispatch(alertActions.info(msg)));
        }
        catch (err){
            dispatch(logOut());
        }
    };
}