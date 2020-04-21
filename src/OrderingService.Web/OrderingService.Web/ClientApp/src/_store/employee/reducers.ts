import { 
    EmployeeState, EmployeeActionTypes, 
    EMPLOYEE_CREATE_SUCCESS, 
    EMPLOYEE_UPDATE_SUCCESS, 
    EMPLOYEE_DELETE_SUCCESS, 
    EMPLOYEE_LOAD_LIST_SUCCESS, 
    EMPLOYEE_LOAD_SUCCESS, 
    EMPLOYEE_SET, 
    EMPLOYEE_CREATE_REQUEST,
    EMPLOYEE_CREATE_FAILURE,
    EMPLOYEE_LOAD_LIST_REQUEST,
    EMPLOYEE_LOAD_LIST_FAILURE,
    EMPLOYEE_UPDATE_REQUEST,
    EMPLOYEE_DELETE_FAILURE,
    EMPLOYEE_UPDATE_FAILURE,
    EMPLOYEE_DELETE_REQUEST,
    EMPLOYEE_LOAD_REQUEST,
    EMPLOYEE_LOAD_FAILURE
} from './types';

const initialState: EmployeeState  = {
    loading: false,
    listLoading: false,
    creating: false,
    updating: false,
    deleting: false,
    employee: undefined,
    list: undefined,
    pagesCount: undefined
};

export function employeeReducer(state : EmployeeState = initialState, action: EmployeeActionTypes): EmployeeState {
    switch(action.type){
        case EMPLOYEE_CREATE_REQUEST:
            return { ...state, creating: true };
        case EMPLOYEE_CREATE_SUCCESS:
            return { ...state, employee: action.employee, creating: false};
        case EMPLOYEE_CREATE_FAILURE:
            return { ...state, creating: false };

        case EMPLOYEE_UPDATE_REQUEST:
            return { ...state, updating: true };
        case EMPLOYEE_UPDATE_SUCCESS:
            return { ...state, employee: action.employee, updating: false };
        case EMPLOYEE_UPDATE_FAILURE:
            return { ...state, updating: false };

        case EMPLOYEE_DELETE_REQUEST:
            return { ...state, deleting: true };
        case EMPLOYEE_DELETE_SUCCESS:
            return { ...state, employee: undefined };
        case EMPLOYEE_DELETE_FAILURE:
            return { ...state, deleting: false };

        case EMPLOYEE_LOAD_LIST_REQUEST:
            return { ...state, listLoading: true };
        case EMPLOYEE_LOAD_LIST_SUCCESS:
            return { ...state, list: action.list, pagesCount: action.pagesCount, listLoading: false };
        case EMPLOYEE_LOAD_LIST_FAILURE:
            return { ...state, listLoading: false };

        case EMPLOYEE_LOAD_REQUEST:
            return { ...state, loading: true };
        case EMPLOYEE_LOAD_SUCCESS:
            return { ...state, employee: action.employee, loading: false };
        case EMPLOYEE_LOAD_FAILURE:
            return { ...state, loading: false };
        
        case EMPLOYEE_SET:
            return { ...state, employee: action.employee };
        
        default:
            return state;
    }
}