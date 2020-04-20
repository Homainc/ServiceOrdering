import { employeeService } from '../_services';
import { employeeConstants } from '../_constants';
import { EmployeeProfileDTO } from '../WebApiModels';
import { ThunkAction } from 'redux-thunk';
import { EmployeeState, EmployeeAction } from '../_reducers/employee.reducer';
import { AuthenticationAction } from '../_reducers/authentication.reducer';
import { userActions } from './user.actions';

type EmployeeThunkResult<R> = ThunkAction<R, EmployeeState, undefined, EmployeeAction | AuthenticationAction>;

const defaultAction: EmployeeAction = {
    type: '',
    employeeProfile: undefined,
    employeeList: undefined,
    pagesCount: undefined,
    error: undefined
};

export const employeeActions = {
    updateEmployeeProfile,
    createEmployeeProfile,
    deleteEmployeeProfile,
    loadEmployees,
    loadEmployeeProfile,
    setEmployeeProfile
};

function setEmployeeProfile(employeeProfile: EmployeeProfileDTO | undefined): EmployeeAction {
    return {
        ...defaultAction,
        type: employeeConstants.EMPLOYEE_PROFILE_SET,
        employeeProfile
    };
}

function createEmployeeProfile(employeeProfile: EmployeeProfileDTO): EmployeeThunkResult<void> {
    return dispatch => {
        dispatch(request(employeeProfile));
        return employeeService.createEmployeeProfile(employeeProfile)
            .then(employeeProfile => {
                dispatch(userActions.updateAuthEmployeeProfile(employeeProfile));
                dispatch(success(employeeProfile));
            },
            error => {
                dispatch(failure(error));
            });
    };

    function request(employeeProfile: EmployeeProfileDTO): EmployeeAction { 
        return { 
            ...defaultAction,
            type: employeeConstants.EMPLOYEE_PROFILE_CREATE_REQUEST,
            employeeProfile
    }; 
    }
    function success(employeeProfile: EmployeeProfileDTO): EmployeeAction { 
        return { 
            ...defaultAction,
            type: employeeConstants.EMPLOYEE_PROFILE_CREATE_SUCCESS,
            employeeProfile
        }; 
    }
    function failure(error: string): EmployeeAction { 
        return { 
            ...defaultAction,
            type: employeeConstants.EMPLOYEE_PROFILE_CREATE_FAILURE,
            error
        }; 
    } 
}

function updateEmployeeProfile(employeeProfile: EmployeeProfileDTO): EmployeeThunkResult<void> {
    return dispatch => {
        dispatch(request(employeeProfile));

        return employeeService.updateEmployeeProfile(employeeProfile)
            .then(employeeProfile => {
                dispatch(userActions.updateAuthEmployeeProfile(employeeProfile));
                dispatch(success(employeeProfile));
            },
            error => {
                dispatch(failure(error));
            });
    };

    function request(employeeProfile: EmployeeProfileDTO): EmployeeAction { 
        return { 
            ...defaultAction,
            type: employeeConstants.EMPLOYEE_PROFILE_UPDATE_REQUEST, 
            employeeProfile 
        }; 
    }
    function success(employeeProfile: EmployeeProfileDTO): EmployeeAction { 
        return { 
            ...defaultAction,
            type: employeeConstants.EMPLOYEE_PROFILE_UPDATE_SUCCESS, 
            employeeProfile 
        }; 
    }
    function failure(error: string): EmployeeAction { 
        return {
            ...defaultAction, 
            type: employeeConstants.EMPLOYEE_PROFILE_UPDATE_FAILURE, 
            error 
        }; 
    } 
}

function deleteEmployeeProfile(employeeProfile: EmployeeProfileDTO): EmployeeThunkResult<void> {
    return dispatch => {
        dispatch(request(employeeProfile));

        return employeeService.deleteEmployeeProfile(employeeProfile.id)
            .then(() => {
                dispatch(userActions.updateAuthEmployeeProfile(undefined));
                dispatch(success());
            },
            error => {
                dispatch(failure(error));
            });
    };

    function request(employeeProfile: EmployeeProfileDTO): EmployeeAction { 
        return { 
            ...defaultAction,
            type: employeeConstants.EMPLOYEE_PROFILE_DELETE_REQUEST, 
            employeeProfile 
        }; 
    }
    function success(): EmployeeAction { 
        return { 
            ...defaultAction,
            type: employeeConstants.EMPLOYEE_PROFILE_DELETE_SUCCESS 
        }; 
    }
    function failure(error: string): EmployeeAction { 
        return { 
            ...defaultAction,
            type: employeeConstants.EMPLOYEE_PROFILE_DELETE_FAILURE, error 
        }; 
    } 
}

function loadEmployees(pageNumber: number): EmployeeThunkResult<void> {
    return dispatch => {
        dispatch(request());

        return employeeService.loadEmployees(pageNumber)
            .then(data => {
                dispatch(success(data.value, data.pagesCount));
            },
            error => {
                dispatch(failure(error));
            });
    };

    function request(): EmployeeAction { 
        return { 
            ...defaultAction,
            type: employeeConstants.EMPLOYEES_LOAD_REQUEST 
        }; 
    }
    function success(employeeList: Array<EmployeeProfileDTO>, pagesCount: number): EmployeeAction { 
        return {
            ...defaultAction, 
            type: employeeConstants.EMPLOYEES_LOAD_SUCCESS, employeeList, pagesCount 
        }; 
    }
    function failure(error: string): EmployeeAction { 
        return { 
            ...defaultAction,
            type: employeeConstants.EMPLOYEES_LOAD_FAILURE, error
         }; 
    } 
}

function loadEmployeeProfile(id: string): EmployeeThunkResult<void> {
    return dispatch => {
        dispatch(request());

        return employeeService.loadEmployeeProfile(id)
            .then(data => {
                dispatch(success(data));
            },
            error => {
                dispatch(failure(error));
            });
    };

    function request(): EmployeeAction { 
        return {
            ...defaultAction,
            type: employeeConstants.EMPLOYEE_PROFILE_LOAD_REQUEST 
        }; 
    }
    function success(employeeProfile: EmployeeProfileDTO): EmployeeAction { 
        return { 
            ...defaultAction,
            type: employeeConstants.EMPLOYEE_PROFILE_LOAD_SUCCESS, employeeProfile 
        }; 
    }
    function failure(error: string): EmployeeAction { 
        return {
            ...defaultAction, 
            type: employeeConstants.EMPLOYEE_PROFILE_LOAD_FAILURE, 
            error 
        }; 
    } 
}