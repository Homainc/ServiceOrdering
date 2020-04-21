import { history, api } from "../../_helpers";
import { ThunkAction } from "redux-thunk";
import { UserDTO } from "../../WebApiModels";
import { 
    ProfileState, ProfileActionTypes, 
    PROFILE_LOAD_REQUEST, PROFILE_LOAD_SUCCESS, PROFILE_LOAD_FAILURE, 
    PROFILE_UPDATE_REQUEST, PROFILE_UPDATE_SUCCESS, PROFILE_UPDATE_FAILURE 
} from "./types";
import * as employee from '../employee/actions';
import * as auth from '../auth/actions';
import { EmployeeActionTypes } from "../employee/types";
import { AuthActionTypes } from "../auth/types";

export function load(
): ThunkAction<void, ProfileState, undefined, ProfileActionTypes | EmployeeActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            const profile = (await api.Account_GetProfile({})).body as UserDTO;

            dispatch(success(profile));
            dispatch(employee.set(profile.employeeProfile));            
        }
        catch (err) {
            dispatch(failure(err));
            history.push('/login');
        }
    };

    function request(): ProfileActionTypes { 
        return { type: PROFILE_LOAD_REQUEST }; 
    }
    function success(profile: UserDTO): ProfileActionTypes { 
        return { type: PROFILE_LOAD_SUCCESS, profile }; 
    }
    function failure(error: string): ProfileActionTypes {
         return { type: PROFILE_LOAD_FAILURE, error }; 
    } 
}

export function update(
    profile: UserDTO
): ThunkAction<void, ProfileState, undefined, ProfileActionTypes | AuthActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            profile = (await api.Account_UpdateProfile({ id: profile.id as string, userDto: profile })).body as UserDTO;

            dispatch(auth.updateUser(profile));
            dispatch(success(profile));
        }
        catch (err) {
            dispatch(failure(err));
        }
    };

    function request(): ProfileActionTypes { 
        return { type: PROFILE_UPDATE_REQUEST }; 
    }
    function success(profile: UserDTO): ProfileActionTypes { 
        return { type: PROFILE_UPDATE_SUCCESS, profile }; 
    }
    function failure(error: string): ProfileActionTypes { 
        return { type: PROFILE_UPDATE_FAILURE, error }; 
    } 
}