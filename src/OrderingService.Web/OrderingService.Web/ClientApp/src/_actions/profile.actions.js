import { profileConstants } from "../_constants";
import { profileService } from "../_services";
import { history } from "../_helpers";
import { userActions } from "./user.actions";
import { employeeActions } from "./employee.actions";

export const profileActions = {
    loadProfile,
    updateProfile,
};

function loadProfile(){
    return dispatch => {
        dispatch(request());

        profileService.loadProfile()
            .then(profile => {
                dispatch(success(profile));
                dispatch(employeeActions.setEmployeeProfile(profile.employeeProfile));
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
                dispatch(userActions.updateAuthUser(profile));
                dispatch(success(profile));
            },
            error => {
                dispatch(failure(error));
            });
    };

    function request() { return { type: profileConstants.PROFILE_UPDATE_REQUEST, profile }; }
    function success(profile) { return { type: profileConstants.PROFILE_UPDATE_SUCCESS, profile, employeeProfile: profile.employeeProfile }; }
    function failure(error) { return { type: profileConstants.PROFILE_UPDATE_FAILURE, error }; } 
}