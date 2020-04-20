import { userConstants, authenticationConstants } from '../_constants';
import { UserDTO, EmployeeProfileDTO } from '../WebApiModels';

type AuthenticationState = {
    loggingIn: boolean;
    signingUp: boolean;
    loggedIn: boolean;
    user: UserDTO | undefined;
};

type AuthenticationAction = {
    type: string;
    user: UserDTO | undefined;
    employeeProfile: EmployeeProfileDTO | undefined;
};

let user = JSON.parse(localStorage.getItem('user') as string);
const initialState: AuthenticationState = user ? 
{ 
    signingUp: false,
    loggingIn: false,
    loggedIn: true, 
    user 
} : { 
    signingUp: false,
    loggingIn: false,
    loggedIn: false,
    user: undefined 
};

export function authentication(state: AuthenticationState = initialState, action: AuthenticationAction): AuthenticationState {
    switch (action.type){
        // LOG IN
        case userConstants.LOGIN_REQUEST:
            return {
                ...state,
                loggingIn: true,
                user: action.user
            };
        case userConstants.LOGIN_SUCCESS:
            return {
                ...state,
                loggedIn: true,
                loggingIn: false,
                user: action.user
            };
        case userConstants.LOGIN_FAILURE:
            return {
                ...state,
                loggedIn: false,
                loggingIn: false,
                user: undefined
            };
        case userConstants.LOGOUT:
            return {
                ...state,
                loggedIn: false,
                user: undefined
            };

        // SIGN UP
        case userConstants.SIGN_UP_REQUEST:
            return {
                ...state,
                signingUp: true,
                user: action.user
            };
        case userConstants.SIGN_UP_SUCCESS:
            return {
                ...state,
                signingUp: false,
                loggedIn: true,
                user: action.user
            };
        case userConstants.SIGN_UP_FAILURE:
            return {
                ...state,
                signingUp: false,
                loggedIn: false,
                user: undefined
            };

        case authenticationConstants.AUTH_UPDATE_USER:
            return {
                ...state,
                user: {...action.user, token: state.user && state.user.token }
            };
        case authenticationConstants.AUTH_UPDATE_EMPLOYEE_PROFILE:
            return {
                ...state,
                user: {...state.user, employeeProfile: action.employeeProfile }
            };
        
        default:
            return state;
        
    }
}