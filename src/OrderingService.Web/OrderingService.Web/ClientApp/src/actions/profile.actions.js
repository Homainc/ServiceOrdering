import { profileConstants } from "../constants";
import { profileService } from "../services";
import { history } from "../helpers";

export const profileActions = {
    loadProfile,
    updateProfile,
    createEmployeeProfile,
    updateEmployeeProfile
};

function loadProfile(){
    return dispatch => {
        dispatch(request());

        profileService.loadProfile()
            .then(profile => {
                dispatch(success(profile));
            },
            () => {
                dispatch(failure());
                history.push('/login');
            });
    };

    function request() { return { type: profileConstants.PROFILE_REQUEST }; }
    function success(profile) { return { type: profileConstants.PROFILE_SUCCESS, profile }; }
    function failure() { return { type: profileConstants.PROFILE_FAILURE }; } 
}

function updateProfile(profile){
    return dispatch => {
        dispatch(request(profile));

        return profileService.updateProfile(profile)
            .then(profile => {
                dispatch(success(profile));
            },
            error => {
                dispatch(failure(error));
            });
    };

    function request() { return { type: profileConstants.PROFILE_UPDATE_REQUEST, profile }; }
    function success(profile) { return { type: profileConstants.PROFILE_UPDATE_SUCCESS, profile }; }
    function failure(error) { return { type: profileConstants.PROFILE_UPDATE_FAILURE, error }; } 
}

function createEmployeeProfile(employeeProfile){
    return dispatch => {
        dispatch(request(employeeProfile));

        return profileService.createEmployeeProfile(employeeProfile)
            .then(employeeProfile => {
                dispatch(success(employeeProfile));
            },
            error => {
                dispatch(failure(error));
            });
    };

    function request() { return { type: profileConstants.EMPLOYEE_PROFILE_CREATE_REQUEST, employeeProfile }; }
    function success(employeeProfile) { return { type: profileConstants.EMPLOYEE_PROFILE_CREATE_SUCCESS, employeeProfile }; }
    function failure(error) { return { type: profileConstants.EMPLOYEE_PROFILE_CREATE_FAILURE, error }; } 
}

function updateEmployeeProfile(employeeProfile){
    return dispatch => {
        dispatch(request(employeeProfile));

        return profileService.updateEmployeeProfile(employeeProfile)
            .then(employeeProfile => {
                dispatch(success(employeeProfile));
            },
            error => {
                dispatch(failure(error));
            });
    };

    function request() { return { type: profileConstants.EMPLOYEE_PROFILE_UPDATE_REQUEST, employeeProfile }; }
    function success(employeeProfile) { return { type: profileConstants.EMPLOYEE_PROFILE_UPDATE_SUCCESS, employeeProfile }; }
    function failure(error) { return { type: profileConstants.EMPLOYEE_PROFILE_UPDATE_FAILURE, error }; } 
}