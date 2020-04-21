import { EmployeeProfileDTO } from "../../WebApiModels";

export const EMPLOYEE_CREATE_REQUEST = 'employee/create (request)';
export const EMPLOYEE_CREATE_SUCCESS = 'employee/create (success)';
export const EMPLOYEE_CREATE_FAILURE = 'employee/create (failure)';

export const EMPLOYEE_UPDATE_REQUEST = 'employee/update (request)';
export const EMPLOYEE_UPDATE_SUCCESS = 'employee/update (success)';
export const EMPLOYEE_UPDATE_FAILURE = 'employee/update (failure)';

export const EMPLOYEE_DELETE_REQUEST = 'employee/delete (request)';
export const EMPLOYEE_DELETE_SUCCESS = 'employee/delete (success)';
export const EMPLOYEE_DELETE_FAILURE = 'employee/delete (failure)';

export const EMPLOYEE_LOAD_LIST_REQUEST = 'employee/load_list (request)';
export const EMPLOYEE_LOAD_LIST_SUCCESS = 'employee/load_list (success)';
export const EMPLOYEE_LOAD_LIST_FAILURE = 'employee/load_list (failure)';

export const EMPLOYEE_LOAD_REQUEST = 'employee/load (request)';
export const EMPLOYEE_LOAD_SUCCESS = 'employee/load (success)';
export const EMPLOYEE_LOAD_FAILURE = 'employee/load (failure)';

export const EMPLOYEE_SET = 'employee/set';

interface EmployeeRequestAction {
    type: 
        typeof EMPLOYEE_CREATE_REQUEST | typeof EMPLOYEE_DELETE_REQUEST | 
        typeof EMPLOYEE_UPDATE_REQUEST | typeof EMPLOYEE_LOAD_LIST_REQUEST |
        typeof EMPLOYEE_LOAD_REQUEST;
};

interface EmployeeCreateSuccessAction {
    type: typeof EMPLOYEE_CREATE_SUCCESS;
    employee: EmployeeProfileDTO;
};

interface EmployeeUpdateSuccessAction {
    type: typeof EMPLOYEE_UPDATE_SUCCESS;
    employee: EmployeeProfileDTO;
};

interface EmployeeDeleteSuccessAction {
    type: typeof EMPLOYEE_DELETE_SUCCESS;
};

interface EmployeeLoadListSuccessAction {
    type: typeof EMPLOYEE_LOAD_LIST_SUCCESS;
    list: Array<EmployeeProfileDTO>;
    pagesCount: number;
};

interface EmployeeLoadSuccessAction {
    type: typeof EMPLOYEE_LOAD_SUCCESS;
    employee: EmployeeProfileDTO;
};

interface EmployeeSetAction {
    type: typeof EMPLOYEE_SET;
    employee: EmployeeProfileDTO | undefined;
};

interface EmployeeFailureAction {
    type: 
        typeof EMPLOYEE_CREATE_FAILURE | typeof EMPLOYEE_UPDATE_FAILURE | 
        typeof EMPLOYEE_DELETE_FAILURE | typeof EMPLOYEE_LOAD_LIST_FAILURE |
        typeof EMPLOYEE_LOAD_FAILURE;
    error: string;
};

export type EmployeeActionTypes = 
    EmployeeRequestAction |
    EmployeeCreateSuccessAction |
    EmployeeUpdateSuccessAction |
    EmployeeDeleteSuccessAction |
    EmployeeLoadListSuccessAction |
    EmployeeLoadSuccessAction |
    EmployeeSetAction |
    EmployeeFailureAction;

export interface EmployeeState {
    listLoading: boolean;
    creating: boolean;
    updating: boolean;
    deleting: boolean;
    list: Array<EmployeeProfileDTO> | undefined;
    employee: EmployeeProfileDTO | undefined;
    pagesCount: number | undefined;
};