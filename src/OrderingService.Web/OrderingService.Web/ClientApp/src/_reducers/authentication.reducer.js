import { userConstants, authenticationConstants } from '../_constants';

let user = JSON.parse(localStorage.getItem('user'));
const initialState = user ? { loggedIn: true, user } : {};

export function authentication(state = initialState, action) {
    switch (action.type){
        // LOG IN
        case userConstants.LOGIN_REQUEST:
            return {
                loggingIn: true,
                user: action.user
            };
        case userConstants.LOGIN_SUCCESS:
            return {
                loggedIn: true,
                user: action.user
            };
        case userConstants.LOGIN_FAILURE:
            return {};
        case userConstants.LOGOUT:
            return {};

        // SIGN UP
        case userConstants.SIGN_UP_REQUEST:
            return {
                signingUp: true,
                user: action.user
            };
        case userConstants.SIGN_UP_SUCCESS:
            return {
                loggedIn: true,
                user: action.user
            };
        case userConstants.SIGN_UP_FAILURE:
            return {};

        case authenticationConstants.AUTH_UPDATE_USER:
            return {
                loggedIn: true,
                user: {...action.user, token: state.user.token }
            };
        case authenticationConstants.AUTH_UPDATE_EMPLOYEE_PROFILE:
            return {
                loggedIn: true,
                user: {...state.user, employeeProfile: action.employeeProfile }
            };
        
        default:
            return state;
        
    }
}