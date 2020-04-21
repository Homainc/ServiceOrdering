import { api } from '../../_helpers';
import { EmployeeProfileDTO } from '../../WebApiModels';
import { ThunkAction } from 'redux-thunk';
import { 
    EmployeeActionTypes, EmployeeState,
    EMPLOYEE_SET, 
    EMPLOYEE_CREATE_REQUEST, EMPLOYEE_CREATE_SUCCESS, EMPLOYEE_CREATE_FAILURE, 
    EMPLOYEE_UPDATE_REQUEST, EMPLOYEE_UPDATE_SUCCESS, EMPLOYEE_UPDATE_FAILURE, 
    EMPLOYEE_DELETE_REQUEST, EMPLOYEE_DELETE_SUCCESS, EMPLOYEE_DELETE_FAILURE, 
    EMPLOYEE_LOAD_LIST_REQUEST, EMPLOYEE_LOAD_LIST_SUCCESS, EMPLOYEE_LOAD_LIST_FAILURE, 
    EMPLOYEE_LOAD_REQUEST, EMPLOYEE_LOAD_SUCCESS, EMPLOYEE_LOAD_FAILURE 
} from './types';
import * as auth from '../auth/actions';
import { AuthActionTypes } from '../auth/types';
import { PagedResult } from '../types';
import { RootState } from '..';

export function set(employee: EmployeeProfileDTO | undefined): EmployeeActionTypes {
    return { type: EMPLOYEE_SET, employee };
}

export function create(
    employee: EmployeeProfileDTO
): ThunkAction<void, EmployeeState, undefined, EmployeeActionTypes | AuthActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            employee = (await api.EmployeeProfile_Create({ employeeProfileDto: employee })).body as EmployeeProfileDTO;
            
            dispatch(auth.updateEmployee(employee));
            dispatch(success(employee));
        }
        catch (err) {
            dispatch(failure(err));
        }
    };

    function request(): EmployeeActionTypes { 
        return { type: EMPLOYEE_CREATE_REQUEST }; 
    }
    function success(employee: EmployeeProfileDTO): EmployeeActionTypes { 
        return { type: EMPLOYEE_CREATE_SUCCESS, employee }; 
    }
    function failure(error: string): EmployeeActionTypes { 
        return { type: EMPLOYEE_CREATE_FAILURE, error }; 
    } 
}

export function update(
    employee: EmployeeProfileDTO
): ThunkAction<void, EmployeeState, undefined, EmployeeActionTypes | AuthActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            employee = (await api.EmployeeProfile_Update({ id: employee.id as string, employeeProfileDto: employee })).body as EmployeeProfileDTO;
            
            dispatch(auth.updateEmployee(employee));
            dispatch(success(employee));
        }
        catch (err) {
            dispatch(failure(err));
        }
    };

    function request(): EmployeeActionTypes { 
        return { type: EMPLOYEE_UPDATE_REQUEST }; 
    }
    function success(employee: EmployeeProfileDTO): EmployeeActionTypes { 
        return { type: EMPLOYEE_UPDATE_SUCCESS, employee }; 
    }
    function failure(error: string): EmployeeActionTypes { 
        return { type: EMPLOYEE_UPDATE_FAILURE, error }; 
    } 
}

export function deleteById(
    id: string
): ThunkAction<void, EmployeeState, undefined, EmployeeActionTypes | AuthActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            await api.EmployeeProfile_Delete({ id });

            dispatch(auth.updateEmployee(undefined));
            dispatch(success());
        }
        catch (err) {
            dispatch(failure(err));
        }
    };

    function request(): EmployeeActionTypes { 
        return { type: EMPLOYEE_DELETE_REQUEST }; 
    }
    function success(): EmployeeActionTypes { 
        return { type: EMPLOYEE_DELETE_SUCCESS }; 
    }
    function failure(error: string): EmployeeActionTypes { 
        return { type: EMPLOYEE_DELETE_FAILURE, error }; 
    } 
}

export function loadList(
    pageNumber: number
): ThunkAction<void, EmployeeState, undefined, EmployeeActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            const pagedResult = (await api.EmployeeProfile_GetEmployees({ pageNumber })).body as PagedResult<EmployeeProfileDTO>;
            
            dispatch(success(pagedResult));
        }
        catch (err) {
            dispatch(failure(err));
        }
    };

    function request(): EmployeeActionTypes { 
        return { type: EMPLOYEE_LOAD_LIST_REQUEST }; 
    }
    function success(pagedResult: PagedResult<EmployeeProfileDTO>): EmployeeActionTypes { 
        return { type: EMPLOYEE_LOAD_LIST_SUCCESS, list: pagedResult.value, pagesCount: pagedResult.pagesCount }; 
    }
    function failure(error: string): EmployeeActionTypes { 
        return { type: EMPLOYEE_LOAD_LIST_FAILURE, error }; 
    } 
}

export function load(
    id: string
): ThunkAction<void, RootState, undefined, EmployeeActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            const employee = (await api.EmployeeProfile_GetEmployeeById({ id })).body as EmployeeProfileDTO;
            
            dispatch(success(employee));
        }
        catch (err) {
            dispatch(failure(err));
        }
    };

    function request(): EmployeeActionTypes { 
        return { type: EMPLOYEE_LOAD_REQUEST }; 
    }
    function success(employee: EmployeeProfileDTO): EmployeeActionTypes { 
        return { type: EMPLOYEE_LOAD_SUCCESS, employee }; 
    }
    function failure(error: string): EmployeeActionTypes { 
        return { type: EMPLOYEE_LOAD_FAILURE, error }; 
    } 
}