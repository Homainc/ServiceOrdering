import { userConstants } from '../constants';

let user = JSON.parse(localStorage.getItem('user'));
const initialState = user ? { loggedIn: true, user } : {};

export function authentication(state = initialState, action) {
    switch (action.type){
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
        default:
            return state;
        
    }
}