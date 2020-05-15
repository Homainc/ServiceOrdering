import { api, getErrorMessageFromEx } from '../../_helpers';
import { EmployeeProfileDto, EmployeeProfileCreateDto, EmployeeProfileUpdateDto, IPagedResultOfEmployeeProfileDto } from '../../WebApiModels';
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
import * as alertActions from '../alert/actions';
import { AlertActionTypes } from '../alert/types';
import { AuthActionTypes } from '../auth/types';
import { RootState } from '..';

export function set(employee: EmployeeProfileDto | undefined): EmployeeActionTypes {
    return { type: EMPLOYEE_SET, employee };
}

export function create(
    employee: EmployeeProfileCreateDto
): ThunkAction<void, RootState, undefined, EmployeeActionTypes | AuthActionTypes | AlertActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            const createdEmployee = (await api.EmployeeProfile_Create({ employeeProfileDto: employee })).body as EmployeeProfileDto;
            
            dispatch(auth.updateEmployee(createdEmployee));
            dispatch(alertActions.success('Employee profile was successfully created!'));
            dispatch(success(createdEmployee));
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
        }
    };

    function request(): EmployeeActionTypes { 
        return { type: EMPLOYEE_CREATE_REQUEST }; 
    }
    function success(employee: EmployeeProfileDto): EmployeeActionTypes { 
        return { type: EMPLOYEE_CREATE_SUCCESS, employee }; 
    }
    function failure(error: string): EmployeeActionTypes { 
        return { type: EMPLOYEE_CREATE_FAILURE, error }; 
    } 
}

export function update(
    employee: EmployeeProfileUpdateDto
): ThunkAction<void, RootState, undefined, EmployeeActionTypes | AuthActionTypes | AlertActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            const updatedEmployee = (await api.EmployeeProfile_Update({ id: employee.id as string, employeeProfileDto: employee })).body as EmployeeProfileDto;
            
            dispatch(auth.updateEmployee(updatedEmployee));
            dispatch(alertActions.success('Employee profile was successfully updated!'));
            dispatch(success(updatedEmployee));
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
        }
    };

    function request(): EmployeeActionTypes { 
        return { type: EMPLOYEE_UPDATE_REQUEST }; 
    }
    function success(employee: EmployeeProfileDto): EmployeeActionTypes { 
        return { type: EMPLOYEE_UPDATE_SUCCESS, employee }; 
    }
    function failure(error: string): EmployeeActionTypes { 
        return { type: EMPLOYEE_UPDATE_FAILURE, error }; 
    } 
}

export function deleteById(
    id: string
): ThunkAction<void, RootState, undefined, EmployeeActionTypes | AuthActionTypes | AlertActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            await api.EmployeeProfile_Delete({ id });

            dispatch(auth.updateEmployee(undefined));
            dispatch(alertActions.success('Employee profile was successfully deleted!'));
            dispatch(success());
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
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
    pageNumber: number, 
    searchString?: string, 
    serviceTypeId?: number, 
    maxServiceCost?: number
): ThunkAction<void, EmployeeState, undefined, EmployeeActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            const pagedResult = (
                await api.EmployeeProfile_GetEmployees({
                    searchString, 
                    pageNumber,
                    maxServiceCost,
                    serviceTypeId
                })
            ).body as IPagedResultOfEmployeeProfileDto;
            
            dispatch(success(pagedResult));
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
        }
    };

    function request(): EmployeeActionTypes { 
        return { type: EMPLOYEE_LOAD_LIST_REQUEST }; 
    }
    function success(pagedResult: IPagedResultOfEmployeeProfileDto): EmployeeActionTypes { 
        return { type: EMPLOYEE_LOAD_LIST_SUCCESS, list: pagedResult.value as EmployeeProfileDto[], pagesCount: pagedResult.pagesCount }; 
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
            const employee = (await api.EmployeeProfile_GetEmployeeById({ id })).body as EmployeeProfileDto;
            
            dispatch(success(employee));
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
        }
    };

    function request(): EmployeeActionTypes { 
        return { type: EMPLOYEE_LOAD_REQUEST }; 
    }
    function success(employee: EmployeeProfileDto): EmployeeActionTypes { 
        return { type: EMPLOYEE_LOAD_SUCCESS, employee }; 
    }
    function failure(error: string): EmployeeActionTypes { 
        return { type: EMPLOYEE_LOAD_FAILURE, error }; 
    } 
}