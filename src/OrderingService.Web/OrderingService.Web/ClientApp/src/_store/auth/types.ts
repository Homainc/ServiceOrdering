import { UserDto, UserAuthDto, EmployeeProfileDto, ProblemDetails } from '../../WebApiModels';

export const AUTH_UPDATE_EMPLOYEE = 'auth/update_employee';
export const AUTH_UPDATE_USER = 'auth/update_user';

export const AUTH_LOGIN_REQUEST = 'auth/login (request)';
export const AUTH_LOGIN_SUCCESS = 'auth/login (success)';
export const AUTH_LOGIN_FAILURE = 'auth/login (failure)';

export const AUTH_LOGOUT = 'auth/logout';

export const AUTH_SIGN_UP_REQUEST = 'auth/sign_up (request)';
export const AUTH_SIGN_UP_SUCCESS = 'auth/sign_up (success)';
export const AUTH_SIGN_UP_FAILURE = 'auth/sign_up (failure)';

interface AuthUpdateEmployeeAction {
    type: typeof AUTH_UPDATE_EMPLOYEE;
    employee?: EmployeeProfileDto;
};

interface AuthUpdateUserAction {
    type: typeof AUTH_UPDATE_USER;
    user: UserDto;
};

interface AuthRequestAction {
    type: typeof AUTH_LOGIN_REQUEST | typeof AUTH_SIGN_UP_REQUEST;
};

interface AuthLoginSuccessAction {
    type: typeof AUTH_LOGIN_SUCCESS;
    user: UserAuthDto;
};

interface AuthLogoutAction {
    type: typeof AUTH_LOGOUT;
};

interface AuthSignUpSuccessAction {
    type: typeof AUTH_SIGN_UP_SUCCESS;
    user: UserAuthDto;
};

interface AuthFailureAction {
    type: typeof AUTH_SIGN_UP_FAILURE | typeof AUTH_LOGIN_FAILURE;
    error: ProblemDetails;
};

export type AuthActionTypes =
    AuthRequestAction |
    AuthUpdateEmployeeAction | 
    AuthUpdateUserAction | 
    AuthLoginSuccessAction | 
    AuthLogoutAction |
    AuthSignUpSuccessAction |
    AuthFailureAction; 

export interface AuthState {
    loggedIn: boolean;
    user: UserAuthDto | undefined;
    loggingIn: boolean;
    signingUp: boolean;
};