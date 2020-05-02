import { 
    ServiceTypeState, ServiceTypeActionTypes, 
    SERVICE_TYPE_ORDERED_LIST_REQUEST, ServiceTypeDTO, SERVICE_TYPE_ORDERED_LIST_SUCCESS, SERVICE_TYPE_ORDERED_LIST_FAILURE 
} from "./types";
import { ThunkAction } from "redux-thunk";
import { getErrorMessageFromEx, api } from '../../_helpers';

export function getAllOrderedByProfilesCount(
): ThunkAction<void, ServiceTypeState, undefined, ServiceTypeActionTypes>{
    return async dispatch => {
        dispatch(request());
        try {
            const list = (await api.ServiceType_GetAllOrderedByProfilesCount({})).body as unknown as Array<ServiceTypeDTO>;
            
            dispatch(success(list));
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
        }
    };
    
    function request(): ServiceTypeActionTypes {
        return { type: SERVICE_TYPE_ORDERED_LIST_REQUEST };
    }
    function success(list: Array<ServiceTypeDTO>): ServiceTypeActionTypes {
        return { type: SERVICE_TYPE_ORDERED_LIST_SUCCESS, list };
    }
    function failure(error: string): ServiceTypeActionTypes {
        return { type: SERVICE_TYPE_ORDERED_LIST_FAILURE, error };
    }
}