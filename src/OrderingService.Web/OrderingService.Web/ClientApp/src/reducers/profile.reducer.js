import { profileConstants } from '../constants';

let user = JSON.parse(localStorage.getItem('user'));
const initialState = user ? { profile: user } : {};

export function profile(state = initialState, action) {
    switch(action.type){
        // GET PROFILE
        case profileConstants.PROFILE_REQUEST:
            return {
                profileLoading: true
            };
        case profileConstants.PROFILE_SUCCESS:
            return {
                profile: action.profile
            };
        case profileConstants.PROFILE_FAILURE:
            return {};


        // UPDATE PROFILE
        case profileConstants.PROFILE_UPDATE_REQUEST:
            return {
                profileUpdating: true,
                profile: action.profile
            };
        case profileConstants.PROFILE_UPDATE_SUCCESS:
            return {
                profile: action.profile
            };
        case profileConstants.PROFILE_UPDATE_FAILURE:
            return {};

        default:
            return state;
    }
}