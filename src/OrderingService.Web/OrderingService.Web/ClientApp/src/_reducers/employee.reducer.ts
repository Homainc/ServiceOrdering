import { employeeConstants } from '../_constants';
import { EmployeeProfileDTO } from '../WebApiModels';

type EmployeeState = {
    listLoading: boolean;
    employeeProcessing: boolean;
    employeeDeleting: boolean;
    employeeProfileLoading: boolean;
    employeeList: Array<EmployeeProfileDTO> | undefined;
    employeeProfile: EmployeeProfileDTO | undefined;
    pagesCount: number | undefined;
};

type EmployeeAction = {
    type: string;
    employeeProfile: EmployeeProfileDTO | undefined;
    employeeList: Array<EmployeeProfileDTO> | undefined;
    pagesCount: number | undefined;
};

const initialState: EmployeeState = {
    listLoading: false,
    employeeProcessing: false,
    employeeProfileLoading: false,
    employeeDeleting: false,
    employeeProfile: undefined,
    employeeList: undefined,
    pagesCount: undefined
};

export function employee(state : EmployeeState = initialState, action: EmployeeAction): EmployeeState {
    switch(action.type){
    // CREATE EMPLOYEE PROFILE
        case employeeConstants.EMPLOYEE_PROFILE_CREATE_REQUEST:
            return {
                ...state,
                employeeProcessing: true,
                employeeProfile: action.employeeProfile
            };
        case employeeConstants.EMPLOYEE_PROFILE_CREATE_SUCCESS:
            return {
                ...state,
                employeeProcessing: false,
                employeeProfile: action.employeeProfile
            };
        case employeeConstants.EMPLOYEE_PROFILE_CREATE_FAILURE:
            return {
                ...state,
                employeeProcessing: false,
                employeeProfile: undefined
            };

        // UPDATE EMPLOYEE PROFILE
        case employeeConstants.EMPLOYEE_PROFILE_UPDATE_REQUEST:
            return {
                ...state,
                employeeProcessing: true,
                employeeProfile: action.employeeProfile
            };
        case employeeConstants.EMPLOYEE_PROFILE_UPDATE_SUCCESS:
            return {
                ...state,
                employeeProcessing: false,
                employeeProfile: action.employeeProfile
            };
        case employeeConstants.EMPLOYEE_PROFILE_UPDATE_FAILURE:
            return {
                ...state,
                employeeProcessing: false
            };
        
        // DELETE EMPLOYEE PROFILE
        case employeeConstants.EMPLOYEE_PROFILE_DELETE_REQUEST:
            return {
                ...state,
                employeeDeleting: true
            };
        case employeeConstants.EMPLOYEE_PROFILE_DELETE_SUCCESS:
            return {
                ...state,
                employeeDeleting: false,
                employeeProfile: undefined
            };
        case employeeConstants.EMPLOYEE_PROFILE_DELETE_FAILURE:
            return {
                ...state,
                employeeDeleting: false
            };
        
        // LIST EMPLOYEE PROFILE
        case employeeConstants.EMPLOYEES_LOAD_REQUEST:
            return {
                ...state,
                listLoading: true,
                employeeList: undefined,
                pagesCount: undefined
            };
        case employeeConstants.EMPLOYEES_LOAD_SUCCESS:
            return {
                ...state,
                employeeList: action.employeeList,
                pagesCount: action.pagesCount,
                listLoading: false
            };
        case employeeConstants.EMPLOYEES_LOAD_FAILURE:
            return {
                ...state,
                listLoading: false
            };

        // LOAD EMPLOYEE PROFILE
        case employeeConstants.EMPLOYEE_PROFILE_LOAD_REQUEST:
            return {
                ...state,
                employeeProfileLoading: true,
                employeeProfile: undefined
            };
        case employeeConstants.EMPLOYEE_PROFILE_LOAD_SUCCESS:
            return {
                ...state,
                employeeProfileLoading: false,
                employeeProfile: action.employeeProfile
            };
        case employeeConstants.EMPLOYEE_PROFILE_LOAD_FAILURE:
            return {
                ...state,
                employeeProfileLoading: false
            };

        // SET EMPLOYEE PROFILE
        case employeeConstants.EMPLOYEE_PROFILE_SET:
            return {
                ...state,
                employeeProfile: action.employeeProfile
            };

        default:
            return state;
    }
}