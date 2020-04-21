import { 
    AuthState, AuthActionTypes, 
    AUTH_LOGIN_REQUEST, AUTH_LOGIN_SUCCESS, AUTH_LOGIN_FAILURE, 
    AUTH_LOGOUT, 
    AUTH_SIGN_UP_REQUEST, AUTH_SIGN_UP_SUCCESS, AUTH_SIGN_UP_FAILURE, 
    AUTH_UPDATE_USER, AUTH_UPDATE_EMPLOYEE 
} from "./types";

const user = JSON.parse(localStorage.getItem('user') as string);
const initialState: AuthState = user ? 
{ 
    loggedIn: true, 
    user 
} : { 
    loggedIn: false,
    user: undefined 
};

export function authReducer(state: AuthState = initialState, action: AuthActionTypes): AuthState {
    switch (action.type){
        // LOG IN
        case AUTH_LOGIN_REQUEST:
            return {
                ...state,
                loggedIn: false,
                user: undefined
            };
        case AUTH_LOGIN_SUCCESS:
            return {
                ...state,
                loggedIn: true,
                user: action.user
            };
        case AUTH_LOGIN_FAILURE:
            return {
                ...state,
                loggedIn: false,
                user: undefined
            };
        case AUTH_LOGOUT:
            return {
                ...state,
                loggedIn: false,
                user: undefined
            };

        // SIGN UP
        case AUTH_SIGN_UP_REQUEST:
            return {
                ...state,
                loggedIn: false,
                user: undefined
            };
        case AUTH_SIGN_UP_SUCCESS:
            return {
                ...state,
                loggedIn: true,
                user: action.user
            };
        case AUTH_SIGN_UP_FAILURE:
            return {
                ...state,
                loggedIn: false,
                user: undefined
            };

        case AUTH_UPDATE_USER:
            return {
                ...state,
                user: {...action.user, token: state.user && state.user.token }
            };
        case AUTH_UPDATE_EMPLOYEE:
            return {
                ...state,
                user: {...state.user, employeeProfile: action.employee }
            };
        
        default:
            return state;
        
    }
}