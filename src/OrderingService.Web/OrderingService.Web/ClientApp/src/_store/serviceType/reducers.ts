import { 
    ServiceTypeState, ServiceTypeActionTypes, 
    SERVICE_TYPE_ORDERED_LIST_REQUEST, SERVICE_TYPE_ORDERED_LIST_SUCCESS, SERVICE_TYPE_ORDERED_LIST_FAILURE, SERVICE_TYPE_CREATE_SUCCESS, SERVICE_TYPE_CREATE_FAILURE, SERVICE_TYPE_CREATE_REQUEST, SERVICE_TYPE_DELETE_REQUEST, SERVICE_TYPE_DELETE_SUCCESS, SERVICE_TYPE_DELETE_FAILURE, SERVICE_TYPE_UPDATE_REQUEST, SERVICE_TYPE_UPDATE_SUCCESS, SERVICE_TYPE_UPDATE_FAILURE 
} from './types';

const initialState: ServiceTypeState = {
    listLoading: false,
    list: undefined,
    isServiceProcessing: false
};

export function serviceTypeReducer(state: ServiceTypeState = initialState, action: ServiceTypeActionTypes): ServiceTypeState {
    switch(action.type){
        case SERVICE_TYPE_ORDERED_LIST_REQUEST:
            return { ...state, listLoading: true };
        case SERVICE_TYPE_ORDERED_LIST_SUCCESS:
            return { ...state, listLoading: false, list: action.list };
        case SERVICE_TYPE_ORDERED_LIST_FAILURE:
            return { ...state, listLoading: false };
        
        case SERVICE_TYPE_CREATE_REQUEST:
            return { ...state, isServiceProcessing: true };
        case SERVICE_TYPE_CREATE_SUCCESS:
            state.list?.push(action.service);
            return { ...state, isServiceProcessing: false };
        case SERVICE_TYPE_CREATE_FAILURE:
            return { ...state, isServiceProcessing: false };

        case SERVICE_TYPE_UPDATE_REQUEST:
            return { ...state, isServiceProcessing: true };
        case SERVICE_TYPE_UPDATE_SUCCESS:
            const indUpdated = state.list?.findIndex(x => x.id === action.service.id);
            if(indUpdated && state.list)
                state.list[indUpdated].name = action.service.name;
            return { ...state, isServiceProcessing: false };
        case SERVICE_TYPE_UPDATE_FAILURE:
            return { ...state, isServiceProcessing: false };

        case SERVICE_TYPE_DELETE_REQUEST:
            return { ...state, isServiceProcessing: true };
        case SERVICE_TYPE_DELETE_SUCCESS:
            const ind = state.list?.findIndex(x => x.id === action.id) as number;
            return { ...state, list: state.list?.slice(0, ind).concat(state.list?.splice(ind+1, state.list.length)), isServiceProcessing: false };
        case SERVICE_TYPE_DELETE_FAILURE:
            return { ...state, isServiceProcessing: false };

        default:
            return state;
    }
}