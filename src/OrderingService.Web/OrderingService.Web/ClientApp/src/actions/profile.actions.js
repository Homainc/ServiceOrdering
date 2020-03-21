import { profileConstants } from "../constants";
import { profileService } from "../services";
import { history } from "../helpers";

export const profileActions = {
    loadProfile,
    updateProfile
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
    function failure() { return { type: profileConstants.PROFILE_FAILURE } } 
}

function updateProfile(){

}