import { profileConstants } from '../_constants';
import { UserDTO } from '../WebApiModels';

type ProfileState = {
    profileUpdating: boolean;
    profileLoading: boolean;
    profile: UserDTO | undefined;
};

type ProfileAction = {
    type: string;
    profile: UserDTO | undefined;
};

const initialState: ProfileState = {
    profileUpdating: false,
    profileLoading: false,
    profile: undefined
};

export function profile(state: ProfileState = initialState, action: ProfileAction): ProfileState {
    switch(action.type){
        // GET PROFILE
        case profileConstants.PROFILE_REQUEST:
            return {
                ...state,
                profileLoading: true,
                profile: undefined
            };
        case profileConstants.PROFILE_SUCCESS:
            return {
                ...state,
                profileLoading: false,
                profile: action.profile
            };
        case profileConstants.PROFILE_FAILURE:
            return {
                ...state,
                profileLoading: false
            };


        // UPDATE PROFILE
        case profileConstants.PROFILE_UPDATE_REQUEST:
            return {
                ...state,
                profileUpdating: true,
                profile: action.profile
            };
        case profileConstants.PROFILE_UPDATE_SUCCESS:
            return {
                ...state,
                profileUpdating: false,
                profile: action.profile
            };
        case profileConstants.PROFILE_UPDATE_FAILURE:
            return {
                ...state,
                profileUpdating: false
            };
        
        default:
            return state;
    }
}