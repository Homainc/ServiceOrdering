import { userConstants, authenticationConstants } from "../_constants";
import { userService } from "../_services";
import { alertActions } from ".";
import { history } from "../_helpers";

export const userActions = {
    login,
    logout,
    signUp,
    updateAuthUser,
    updateAuthEmployeeProfile
};

function login(username, password){
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

    function request(user) { return { type: userConstants.LOGIN_REQUEST, user } }
    function success(user) { return { type: userConstants.LOGIN_SUCCESS, user } }
    function failure(error) { return { type: userConstants.LOGIN_FAILURE, error } }
}

function logout(){
    userService.logout();
    return { type: userConstants.LOGOUT };
}

function signUp(user){
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

    function request(user) { return { type: userConstants.SIGN_UP_REQUEST, user } }
    function success(user) { return { type: userConstants.SIGN_UP_SUCCESS, user } }
    function failure(error) { return { type: userConstants.SIGN_UP_FAILURE, error } }
}

function updateAuthUser(user){
    userService.updateAuthUser(user);
    return { type: authenticationConstants.AUTH_UPDATE_USER, user };
}

function updateAuthEmployeeProfile(employeeProfile){
    userService.updateAuthEmployeeProfile(employeeProfile);
    return { type: authenticationConstants.AUTH_UPDATE_EMPLOYEE_PROFILE, employeeProfile };
}