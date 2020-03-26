import { employeeService } from '../services';
import { employeeConstants } from '../constants';

export const employeeActions = {
    updateEmployeeProfile,
    createEmployeeProfile,
    deleteEmployeeProfile,
    loadEmployees
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

function loadEmployees(pageNumber){
    return dispatch => {
        dispatch(request());

        return employeeService.loadEmployees(pageNumber)
            .then(data => {
                dispatch(success(data.model, data.pagesCount));
            },
            error => {
                dispatch(failure(error));
            });
    };

    function request() { return { type: employeeConstants.EMPLOYEE_LOAD_REQUEST }; }
    function success(employeeList, pagesCount) { return { type: employeeConstants.EMPLOYEE_LOAD_SUCCESS, employeeList, pagesCount }; }
    function failure(error) { return { type: employeeConstants.EMPLOYEE_LOAD_FAILURE, error }; } 
}