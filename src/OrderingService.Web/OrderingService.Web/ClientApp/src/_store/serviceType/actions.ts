import { 
    ServiceTypeState, ServiceTypeActionTypes, 
    SERVICE_TYPE_ORDERED_LIST_REQUEST, SERVICE_TYPE_ORDERED_LIST_SUCCESS, SERVICE_TYPE_ORDERED_LIST_FAILURE, SERVICE_TYPE_CREATE_REQUEST, SERVICE_TYPE_CREATE_SUCCESS, SERVICE_TYPE_CREATE_FAILURE, SERVICE_TYPE_DELETE_REQUEST, SERVICE_TYPE_DELETE_SUCCESS, SERVICE_TYPE_DELETE_FAILURE, SERVICE_TYPE_UPDATE_REQUEST, SERVICE_TYPE_UPDATE_SUCCESS, SERVICE_TYPE_UPDATE_FAILURE 
} from "./types";
import { ThunkAction } from "redux-thunk";
import { getErrorMessageFromEx, api } from '../../_helpers';
import { ServiceTypeDto, ServiceTypeCreateDto, ValidationProblemDetails, ProblemDetails } from "../../WebApiModels";
import * as alertActions from '../alert/actions';
import { RootState } from "..";

export function getAllOrderedByProfilesCount(
): ThunkAction<void, ServiceTypeState, undefined, ServiceTypeActionTypes>{
    return async dispatch => {
        dispatch(request());
        try {
            const list = (await api.ServiceType_GetAllOrderedByProfilesCount({})).body as Array<ServiceTypeDto>;
            
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
    function success(list: Array<ServiceTypeDto>): ServiceTypeActionTypes {
        return { type: SERVICE_TYPE_ORDERED_LIST_SUCCESS, list };
    }
    function failure(error: string): ServiceTypeActionTypes {
        return { type: SERVICE_TYPE_ORDERED_LIST_FAILURE, error };
    }
}

export function create(
    service: ServiceTypeCreateDto
): ThunkAction<void, RootState, undefined, ServiceTypeActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            var createdService = (await api.ServiceType_Create({ item: service })).body as ServiceTypeDto;
            dispatch(success(createdService));
        }
        catch (err) {
            const errObj = err.response.body as ValidationProblemDetails;
            dispatch(alertActions.error(errObj.detail || ''));
            dispatch(failure(errObj));
            throw errObj.errors;
        }
    }

    function request(): ServiceTypeActionTypes {
        return { type: SERVICE_TYPE_CREATE_REQUEST };
    }
    function success(service: ServiceTypeDto): ServiceTypeActionTypes {
        return { type: SERVICE_TYPE_CREATE_SUCCESS, service };
    }
    function failure(error: ValidationProblemDetails): ServiceTypeActionTypes {
        return { type: SERVICE_TYPE_CREATE_FAILURE, error: error.detail || '' };
    }
}

export function update(
    service: ServiceTypeDto
): ThunkAction<void, RootState, undefined, ServiceTypeActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            var updated = (await api.ServiceType_Update({ item: service, id: service.id.toString() })).body as ServiceTypeDto;
            dispatch(success(updated));
        }
        catch (err) {
            const errObj = err.response.body as ValidationProblemDetails;
            dispatch(alertActions.error(errObj.detail || ''));
            dispatch(failure(errObj));
            throw errObj.errors;
        }
    }

    function request(): ServiceTypeActionTypes {
        return { type: SERVICE_TYPE_UPDATE_REQUEST };
    }
    function success(service: ServiceTypeDto): ServiceTypeActionTypes {
        return { type: SERVICE_TYPE_UPDATE_SUCCESS, service };
    }
    function failure(error: ValidationProblemDetails): ServiceTypeActionTypes {
        return { type: SERVICE_TYPE_UPDATE_FAILURE, error: error.detail || '' };
    }
}

export function deleteByid(
    id: number
): ThunkAction<void, RootState, undefined, ServiceTypeActionTypes> {
    return async dispatch => {
        dispatch(request());
        
        try {
            await api.ServiceType_Delete({ id });
            
            dispatch(success(id));
        }
        catch (err){
            const errObj = err.response.body as ProblemDetails;
            dispatch(alertActions.error(errObj.detail || ''));
            dispatch(failure(errObj));
        }
    };

    function request(): ServiceTypeActionTypes {
        return { type: SERVICE_TYPE_DELETE_REQUEST };
    }
    function success(id: number): ServiceTypeActionTypes {
        return { type: SERVICE_TYPE_DELETE_SUCCESS, id };
    }
    function failure(error: ProblemDetails): ServiceTypeActionTypes {
        return { type: SERVICE_TYPE_DELETE_FAILURE, error: error.detail || '' };
    }
}