import { profileConstants } from "../_constants";
import { profileService } from "../_services";
import { history } from "../_helpers";
import { userActions } from "./user.actions";
import { employeeActions } from "./employee.actions";
import { ThunkAction } from "redux-thunk";
import { ProfileState, ProfileAction } from "../_reducers/profile.reducer";
import { UserDTO } from "../WebApiModels";
import { AuthenticationAction } from "../_reducers/authentication.reducer";
import { EmployeeAction } from "../_reducers/employee.reducer";

type ProfileThunkResult<R> = ThunkAction<R, ProfileState, undefined, ProfileAction | AuthenticationAction | EmployeeAction>;

export const profileActions = {
    loadProfile,
    updateProfile,
};

const defaultAction: ProfileAction = {
    type: '',
    profile: undefined,
    error: undefined,
    employeeProfile: undefined
};

function loadProfile(): ProfileThunkResult<void> {
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

    function request(): ProfileAction { 
        return { 
            ...defaultAction,
            type: profileConstants.PROFILE_REQUEST,
        }; 
    }
    function success(profile: UserDTO): ProfileAction { 
        return { 
            ...defaultAction,
            type: profileConstants.PROFILE_SUCCESS, 
            profile 
        }; 
    }
    function failure(): ProfileAction {
         return { 
             ...defaultAction,
             type: profileConstants.PROFILE_FAILURE,
             profile: undefined 
            }; 
        } 
}

function updateProfile(profile: UserDTO): ProfileThunkResult<void> {
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

    function request(profile: UserDTO): ProfileAction { 
        return { 
            ...defaultAction,
            type: profileConstants.PROFILE_UPDATE_REQUEST, 
            profile 
        }; 
    }
    function success(profile: UserDTO): ProfileAction { 
        return { 
            ...defaultAction,
            type: profileConstants.PROFILE_UPDATE_SUCCESS, 
            profile, 
            employeeProfile: profile.employeeProfile 
        }; 
    }
    function failure(error: string): ProfileAction { 
        return { 
            ...defaultAction,
            type: profileConstants.PROFILE_UPDATE_FAILURE, 
            error 
        }; 
    } 
}