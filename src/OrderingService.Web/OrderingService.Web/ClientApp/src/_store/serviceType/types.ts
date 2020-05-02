export const SERVICE_TYPE_ORDERED_LIST_REQUEST = 'serviceType/ordered_list (request)';
export const SERVICE_TYPE_ORDERED_LIST_SUCCESS = 'serviceType/ordered_list (success)';
export const SERVICE_TYPE_ORDERED_LIST_FAILURE = 'serviceType/ordered_list (failure)';

export type ServiceTypeDTO = {
    id: number,
    name: string
};

interface ServiceTypeRequestAction {
    type: typeof SERVICE_TYPE_ORDERED_LIST_REQUEST;
};

interface ServiceTypeOrderedListSuccessAction {
    type: typeof SERVICE_TYPE_ORDERED_LIST_SUCCESS;
    list: Array<ServiceTypeDTO>;
};

interface ServiceTypeFailureAction {
    type: typeof SERVICE_TYPE_ORDERED_LIST_FAILURE;
    error: string;
};

export type ServiceTypeActionTypes = 
    ServiceTypeRequestAction | 
    ServiceTypeOrderedListSuccessAction |
    ServiceTypeFailureAction;

export interface ServiceTypeState {
    listLoading: boolean;
    list: Array<ServiceTypeDTO> | undefined;
};
