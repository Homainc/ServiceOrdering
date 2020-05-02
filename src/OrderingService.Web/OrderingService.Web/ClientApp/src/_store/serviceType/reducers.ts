import { 
    ServiceTypeState, ServiceTypeActionTypes, 
    SERVICE_TYPE_ORDERED_LIST_REQUEST, SERVICE_TYPE_ORDERED_LIST_SUCCESS, SERVICE_TYPE_ORDERED_LIST_FAILURE 
} from './types';

const initialState: ServiceTypeState = {
    listLoading: false,
    list: undefined
};

export function serviceTypeReducer(state: ServiceTypeState = initialState, action: ServiceTypeActionTypes): ServiceTypeState {
    switch(action.type){
        case SERVICE_TYPE_ORDERED_LIST_REQUEST:
            return { ...state, listLoading: true };
        case SERVICE_TYPE_ORDERED_LIST_SUCCESS:
            return { ...state, listLoading: false, list: action.list };
        case SERVICE_TYPE_ORDERED_LIST_FAILURE:
            return { ...state, listLoading: false };
        
        default:
            return state;
    }
}