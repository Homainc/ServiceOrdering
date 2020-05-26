import { 
    AuthState, AuthActionTypes, 
    AUTH_LOGIN_REQUEST, AUTH_LOGIN_SUCCESS, AUTH_LOGIN_FAILURE, 
    AUTH_LOGOUT, 
    AUTH_SIGN_UP_REQUEST, AUTH_SIGN_UP_SUCCESS, AUTH_SIGN_UP_FAILURE, 
    AUTH_UPDATE_USER, AUTH_UPDATE_EMPLOYEE, AUTH_REFRESHING_TOKEN, AUTH_DONE_REFRESHING_TOKEN, AUTH_UPDATE_TOKEN 
} from "./types";
import { api } from "../../_helpers";
import { UserAuthDto } from "../../WebApiModels";

const user = JSON.parse(localStorage.getItem('user') as string) as UserAuthDto;
(() => {
    if(user)
        api.setRequestHeadersHandler(h => ({ ...h, 'Authorization': 'Bearer ' + user.token }));
})();

const initialState: AuthState = 
{ 
    loggedIn: !!user,
    loggingIn: false,
    signingUp: false, 
    user 
};

export function authReducer(state: AuthState = initialState, action: AuthActionTypes): AuthState {
    switch (action.type){
        case AUTH_LOGIN_REQUEST:
            return { ...state, loggingIn: true };
        case AUTH_LOGIN_SUCCESS:
            return { ...state, loggedIn: true, user: action.user, loggingIn: false};
        case AUTH_LOGIN_FAILURE:
            return { ...state, loggedIn: false, user: undefined, loggingIn: false };
        
        case AUTH_LOGOUT:
            return { ...state, loggedIn: false, user: undefined };

        case AUTH_SIGN_UP_REQUEST:
            return { ...state, signingUp: true };
        case AUTH_SIGN_UP_SUCCESS:
            return { ...state, loggedIn: true, user: action.user, signingUp: false };
        case AUTH_SIGN_UP_FAILURE:
            return { ...state, loggedIn: false, user: undefined, signingUp: false };

        case AUTH_UPDATE_USER:
            return {
                ...state,
                user: {...action.user, token: state.user && state.user.token }
            };
        case AUTH_UPDATE_EMPLOYEE:
            return {
                ...state,
                user: { ...state.user as UserAuthDto, employeeProfile: action.employee }
            };
        
        case AUTH_REFRESHING_TOKEN:
            return { ...state, refreshingPromise: action.refreshingPromise };
        case AUTH_DONE_REFRESHING_TOKEN:
            return { ...state, refreshingPromise: undefined };
        case AUTH_UPDATE_TOKEN:
            return { ...state, user: { ...state.user as UserAuthDto, token: action.token } };

        default:
            return state;
        
    }
}