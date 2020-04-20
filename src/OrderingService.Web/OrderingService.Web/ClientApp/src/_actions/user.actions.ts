import { userConstants, authenticationConstants } from "../_constants";
import { userService } from "../_services";
import { alertActions } from ".";
import { history } from "../_helpers";
import { ThunkAction } from "redux-thunk";
import { AuthenticationState, AuthenticationAction } from "../_reducers/authentication.reducer";
import { UserDTO, EmployeeProfileDTO } from "../WebApiModels";
import { AlertAction } from "../_reducers/alert.reducer";

export const userActions = {
    login,
    logout,
    signUp,
    updateAuthUser,
    updateAuthEmployeeProfile
};

type UserThunkResult<R> = ThunkAction<R, AuthenticationState, undefined, AuthenticationAction | AlertAction>;

function login(username: string, password: string): UserThunkResult<void> {
    return dispatch => {
        dispatch(request({ username }));

        userService.login(username, password)
            .then(user => {
                    dispatch(success(user));
                    history.push('/');
                },
                error => {
                    dispatch(failure(error));
                    dispatch(alertActions.error(error));
                }
            );
    };

    function request(user: UserDTO): AuthenticationAction { 
        return { 
            error: undefined,
            employeeProfile: undefined,
            type: userConstants.LOGIN_REQUEST, 
            user 
        } 
    }
    function success(user: UserDTO): AuthenticationAction { 
        return { 
            employeeProfile: undefined,
            type: userConstants.LOGIN_SUCCESS, 
            user,
            error: undefined 
        } 
    }
    function failure(error: string): AuthenticationAction { 
        return { 
            user: undefined,
            employeeProfile: undefined,
            type: userConstants.LOGIN_FAILURE, 
            error
        } 
    }
}

function logout(): AuthenticationAction {
    userService.logout();
    return { 
        type: userConstants.LOGOUT,
        employeeProfile: undefined,
        error: undefined,
        user: undefined 
    };
}

function signUp(user: UserDTO): UserThunkResult<void> {
    return dispatch => {
        dispatch(request(user));
        userService.signUp(user)
            .then(user => {
                dispatch(success(user));
                history.push('/');
            },
            error => {
                dispatch(failure(error));
                dispatch(alertActions.error(error));
            });
    };

    function request(user: UserDTO): AuthenticationAction { 
        return { 
            type: userConstants.SIGN_UP_REQUEST, 
            user,
            error: undefined,
            employeeProfile: undefined 
        } 
    }
    function success(user: UserDTO): AuthenticationAction { 
        return { 
            type: userConstants.SIGN_UP_SUCCESS, 
            user,
            error: undefined,
            employeeProfile: undefined 
        } 
    }
    function failure(error: string): AuthenticationAction { 
        return { 
            type: userConstants.SIGN_UP_FAILURE,
            user: undefined,
            employeeProfile: undefined, 
            error 
        } 
    }
}

function updateAuthUser(user: UserDTO): AuthenticationAction {
    userService.updateAuthUser(user);
    return { 
        type: authenticationConstants.AUTH_UPDATE_USER,
        employeeProfile: undefined,
        error: undefined, 
        user 
    };
}

function updateAuthEmployeeProfile(employeeProfile: EmployeeProfileDTO | undefined): AuthenticationAction{
    userService.updateAuthEmployeeProfile(employeeProfile);
    return { 
        type: authenticationConstants.AUTH_UPDATE_EMPLOYEE_PROFILE,
        error: undefined,
        user: undefined, 
        employeeProfile 
    };
}