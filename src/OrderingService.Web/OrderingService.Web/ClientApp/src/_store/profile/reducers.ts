import { 
    ProfileState, ProfileActionTypes, 
    PROFILE_LOAD_REQUEST, PROFILE_LOAD_SUCCESS, PROFILE_LOAD_FAILURE, 
    PROFILE_UPDATE_REQUEST, PROFILE_UPDATE_SUCCESS, PROFILE_UPDATE_FAILURE 
} from "./types";

const initialState: ProfileState = {
    updating: false,
    loading: false,
    profile: undefined
};

export function profileReducer(state: ProfileState = initialState, action: ProfileActionTypes): ProfileState {
    switch(action.type){
        // Requests
        case PROFILE_LOAD_REQUEST:
            return { ...state, loading: true };
        case PROFILE_UPDATE_REQUEST:
            return { ...state, updating: true };

        // Failures
        case PROFILE_LOAD_FAILURE:
            return { ...state, loading: false };
        case PROFILE_UPDATE_FAILURE:
            return { ...state, updating: false };

        // Successes
        case PROFILE_LOAD_SUCCESS:
            return { ...state, profile: action.profile, loading: false };
        case PROFILE_UPDATE_SUCCESS:
            return { ...state, profile: action.profile, updating: false };
        
        default:
            return state;
    }
}