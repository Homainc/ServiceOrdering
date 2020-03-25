import { profileConstants } from "../constants";
import { profileService } from "../services";
import { history } from "../helpers";

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
            },
            () => {
                dispatch(failure());
                history.push('/login');
            });
    };

    function request() { return { type: profileConstants.PROFILE_REQUEST }; }
    function success(profile) { return { type: profileConstants.PROFILE_SUCCESS, profile, employeeProfile: profile.employeeProfile }; }
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
    function success(profile) { return { type: profileConstants.PROFILE_UPDATE_SUCCESS, profile, employeeProfile: profile.employeeProfile }; }
    function failure(error) { return { type: profileConstants.PROFILE_UPDATE_FAILURE, error }; } 
}