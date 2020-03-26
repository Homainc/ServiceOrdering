import { employeeConstants } from '../constants';

export function employee(state = {}, action) {
    switch(action.type){
    // CREATE EMPLOYEE PROFILE
        case employeeConstants.EMPLOYEE_PROFILE_CREATE_REQUEST:
            return {
                employeeProcessing: true,
                employeeProfile: action.employeeProfile
            };
        case employeeConstants.EMPLOYEE_PROFILE_CREATE_SUCCESS:
            return {
                employeeProfile: action.employeeProfile
            };
        case employeeConstants.EMPLOYEE_PROFILE_CREATE_FAILURE:
            return {};

        // UPDATE EMPLOYEE PROFILE
        case employeeConstants.EMPLOYEE_PROFILE_UPDATE_REQUEST:
            return {
                employeeProcessing: true,
                employeeProfile: action.employeeProfile
            };
        case employeeConstants.EMPLOYEE_PROFILE_UPDATE_SUCCESS:
            return {
                employeeProfile: action.employeeProfile
            };
        case employeeConstants.EMPLOYEE_PROFILE_UPDATE_FAILURE:
            return {};
        
        // DELETE EMPLOYEE PROFILE
        case employeeConstants.EMPLOYEE_PROFILE_DELETE_REQUEST:
            return {
                employeeDeleting: true,
                employeeProfile: action.employeeProfile
            };
        case employeeConstants.EMPLOYEE_PROFILE_DELETE_SUCCESS:
            return {};
        case employeeConstants.EMPLOYEE_PROFILE_DELETE_FAILURE:
            return {};
        
        // LIST EMPLOYEE PROFILE
        case employeeConstants.EMPLOYEE_LOAD_REQUEST:
            return {
                listLoading: true
            };
        case employeeConstants.EMPLOYEE_LOAD_SUCCESS:
            return {
                employeeList: action.employeeList,
                pagesCount: action.pagesCount
            };
        case employeeConstants.EMPLOYEE_LOAD_FAILURE:
            return {};

        
        default:
            return state;
    }
}