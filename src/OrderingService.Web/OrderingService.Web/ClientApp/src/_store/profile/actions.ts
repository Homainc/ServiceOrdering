import { history, api, getErrorMessageFromEx } from "../../_helpers";
import { ThunkAction } from "redux-thunk";
import { UserDto } from "../../WebApiModels";
import { 
    ProfileState, ProfileActionTypes, 
    PROFILE_LOAD_REQUEST, PROFILE_LOAD_SUCCESS, PROFILE_LOAD_FAILURE, 
    PROFILE_UPDATE_REQUEST, PROFILE_UPDATE_SUCCESS, PROFILE_UPDATE_FAILURE 
} from "./types";
import * as employee from '../employee/actions';
import * as auth from '../auth/actions';
import * as alertActions from '../alert/actions';
import { EmployeeActionTypes } from '../employee/types';
import { AuthActionTypes } from '../auth/types';
import { AlertActionTypes } from '../alert/types';
import { RootState } from "..";

export function load(
): ThunkAction<void, ProfileState, undefined, ProfileActionTypes | EmployeeActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            const profile = (await api.Account_GetProfile({})).body as UserDto;

            dispatch(success(profile));
            dispatch(employee.set(profile.employeeProfile));            
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
            history.push('/login');
        }
    };

    function request(): ProfileActionTypes { 
        return { type: PROFILE_LOAD_REQUEST }; 
    }
    function success(profile: UserDto): ProfileActionTypes { 
        return { type: PROFILE_LOAD_SUCCESS, profile }; 
    }
    function failure(error: string): ProfileActionTypes {
         return { type: PROFILE_LOAD_FAILURE, error }; 
    } 
}

export function update(
    profile: UserDto
): ThunkAction<void, RootState, undefined, ProfileActionTypes | AuthActionTypes | AlertActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            profile = (await api.Account_UpdateProfile({ id: profile.id as string, userDto: profile })).body as UserDto;

            dispatch(auth.updateUser(profile));
            dispatch(alertActions.success('Your profile was successfully updated!'));
            dispatch(success(profile));
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
        }
    };

    function request(): ProfileActionTypes { 
        return { type: PROFILE_UPDATE_REQUEST }; 
    }
    function success(profile: UserDto): ProfileActionTypes { 
        return { type: PROFILE_UPDATE_SUCCESS, profile }; 
    }
    function failure(error: string): ProfileActionTypes { 
        return { type: PROFILE_UPDATE_FAILURE, error }; 
    } 
}