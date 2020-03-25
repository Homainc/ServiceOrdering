import { employeeService } from '../services';
import { employeeConstants } from '../constants';

export const employeeActions = {
    updateEmployeeProfile,
    createEmployeeProfile,
    deleteEmployeeProfile
};

function createEmployeeProfile(employeeProfile){
    return dispatch => {
        dispatch(request(employeeProfile));

        return employeeService.createEmployeeProfile(employeeProfile)
            .then(employeeProfile => {
                dispatch(success(employeeProfile));
            },
            error => {
                dispatch(failure(error));
            });
    };

    function request(employeeProfile) { return { type: employeeConstants.EMPLOYEE_PROFILE_CREATE_REQUEST, employeeProfile }; }
    function success(employeeProfile) { return { type: employeeConstants.EMPLOYEE_PROFILE_CREATE_SUCCESS, employeeProfile }; }
    function failure(error) { return { type: employeeConstants.EMPLOYEE_PROFILE_CREATE_FAILURE, error }; } 
}

function updateEmployeeProfile(employeeProfile){
    return dispatch => {
        dispatch(request(employeeProfile));

        return employeeService.updateEmployeeProfile(employeeProfile)
            .then(employeeProfile => {
                dispatch(success(employeeProfile));
            },
            error => {
                dispatch(failure(error));
            });
    };

    function request(employeeProfile) { return { type: employeeConstants.EMPLOYEE_PROFILE_UPDATE_REQUEST, employeeProfile }; }
    function success(employeeProfile) { return { type: employeeConstants.EMPLOYEE_PROFILE_UPDATE_SUCCESS, employeeProfile }; }
    function failure(error) { return { type: employeeConstants.EMPLOYEE_PROFILE_UPDATE_FAILURE, error }; } 
}

function deleteEmployeeProfile(employeeProfile){
    return dispatch => {
        dispatch(request(employeeProfile));

        return employeeService.deleteEmployeeProfile(employeeProfile.id)
            .then(() => {
                dispatch(success());
            },
            error => {
                dispatch(failure(error));
            });
    };

    function request(employeeProfile) { return { type: employeeConstants.EMPLOYEE_PROFILE_DELETE_REQUEST, employeeProfile }; }
    function success() { return { type: employeeConstants.EMPLOYEE_PROFILE_DELETE_SUCCESS }; }
    function failure(error) { return { type: employeeConstants.EMPLOYEE_PROFILE_DELETE_FAILURE, error }; } 
}