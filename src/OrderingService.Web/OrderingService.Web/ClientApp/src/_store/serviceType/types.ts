import { ServiceTypeDto } from "../../WebApiModels";

export const SERVICE_TYPE_ORDERED_LIST_REQUEST = 'serviceType/ordered_list (request)';
export const SERVICE_TYPE_ORDERED_LIST_SUCCESS = 'serviceType/ordered_list (success)';
export const SERVICE_TYPE_ORDERED_LIST_FAILURE = 'serviceType/ordered_list (failure)';

export const SERVICE_TYPE_CREATE_REQUEST = 'serviceType/create (request)';
export const SERVICE_TYPE_CREATE_SUCCESS = 'serviceType/create (success)';
export const SERVICE_TYPE_CREATE_FAILURE = 'serviceType/create (failure)';

export const SERVICE_TYPE_UPDATE_REQUEST = 'serviceType/update (request)';
export const SERVICE_TYPE_UPDATE_SUCCESS = 'serviceType/update (success)';
export const SERVICE_TYPE_UPDATE_FAILURE = 'serviceType/update (failure)';

export const SERVICE_TYPE_DELETE_REQUEST = 'serviceType/delete (request)';
export const SERVICE_TYPE_DELETE_SUCCESS = 'serviceType/delete (success)';
export const SERVICE_TYPE_DELETE_FAILURE = 'serviceType/delete (failure)';

interface ServiceTypeRequestAction {
    type: 
        typeof SERVICE_TYPE_ORDERED_LIST_REQUEST | 
        typeof SERVICE_TYPE_DELETE_REQUEST | 
        typeof SERVICE_TYPE_CREATE_REQUEST |
        typeof SERVICE_TYPE_UPDATE_REQUEST;
};

interface ServiceTypeOrderedListSuccessAction {
    type: typeof SERVICE_TYPE_ORDERED_LIST_SUCCESS;
    list: Array<ServiceTypeDto>;
};

interface ServiceTypeCreateSuccessAction {
    type: typeof SERVICE_TYPE_CREATE_SUCCESS | typeof SERVICE_TYPE_UPDATE_SUCCESS;
    service: ServiceTypeDto;
};

interface ServiceTypeDeleteSuccessAction {
    type: typeof SERVICE_TYPE_DELETE_SUCCESS;
    id: number;
};

interface ServiceTypeFailureAction {
    type: 
        typeof SERVICE_TYPE_ORDERED_LIST_FAILURE | 
        typeof SERVICE_TYPE_CREATE_FAILURE | 
        typeof SERVICE_TYPE_DELETE_FAILURE |
        typeof SERVICE_TYPE_UPDATE_FAILURE;
    error: string;
};

export type ServiceTypeActionTypes = 
    ServiceTypeRequestAction |
    ServiceTypeCreateSuccessAction |
    ServiceTypeDeleteSuccessAction | 
    ServiceTypeOrderedListSuccessAction |
    ServiceTypeFailureAction;

export interface ServiceTypeState {
    listLoading: boolean;
    list: Array<ServiceTypeDto> | undefined;
    isServiceProcessing: boolean;
};
