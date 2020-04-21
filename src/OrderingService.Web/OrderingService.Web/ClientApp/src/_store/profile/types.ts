import { UserDTO } from "../../WebApiModels";

export const PROFILE_LOAD_REQUEST = 'profile/load (request)';
export const PROFILE_LOAD_SUCCESS = 'profile/load (success)';
export const PROFILE_LOAD_FAILURE = 'profile/load (failure)';

export const PROFILE_UPDATE_REQUEST = 'profile/update (request)';
export const PROFILE_UPDATE_SUCCESS ='profile/update (success)';
export const PROFILE_UPDATE_FAILURE ='profile/update (failure)';

interface ProfileRequestAction {
    type: typeof PROFILE_LOAD_REQUEST | typeof PROFILE_UPDATE_REQUEST;
};

interface ProfileLoadSuccessAction {
    type: typeof PROFILE_LOAD_SUCCESS;
    profile: UserDTO;
};

interface ProfileUpdateSuccessAction {
    type: typeof PROFILE_UPDATE_SUCCESS;
    profile: UserDTO;
};

interface ProfileFailureAction {
    type: typeof PROFILE_LOAD_FAILURE | typeof PROFILE_UPDATE_FAILURE;
    error: string;
}

export type ProfileActionTypes =
    ProfileRequestAction |
    ProfileLoadSuccessAction |
    ProfileUpdateSuccessAction |
    ProfileFailureAction;

export interface ProfileState {
    profile: UserDTO | undefined;
    updating: boolean;
    loading: boolean;
};