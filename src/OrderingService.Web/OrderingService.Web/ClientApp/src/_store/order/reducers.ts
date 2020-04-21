import { 
    OrderState, OrderActionTypes, 
    ORDER_CREATE_REQUEST, ORDER_CREATE_SUCCESS, ORDER_CREATE_FAILURE, 
    ORDER_LOAD_LIST_BY_USER_REQUEST, ORDER_LOAD_LIST_BY_USER_SUCCESS, ORDER_LOAD_LIST_BY_USER_FAILURE, 
    ORDER_LOAD_LIST_BY_EMPLOYEE_REQUEST, ORDER_LOAD_LIST_BY_EMPLOYEE_SUCCESS, ORDER_LOAD_LIST_BY_EMPLOYEE_FAILURE, 
    ORDER_ACCEPT_REQUEST, ORDER_ACCEPT_SUCCESS, ORDER_ACCEPT_FAILURE, 
    ORDER_DECLINE_REQUEST, ORDER_DECLINE_SUCCESS, ORDER_DECLINE_FAILURE, 
    ORDER_CONFIRM_REQUEST, ORDER_CONFIRM_SUCCESS, ORDER_CONFIRM_FAILURE 
} from './types';

const initialState: OrderState = {
    listLoading: false,
    creating: false,
    confirming: false,
    declining: false,
    accepting: false,
    order: undefined,
    orders: undefined,
    pagesCount: undefined,
    totalOrders: undefined
};

export function orderReducer(state: OrderState = initialState, action: OrderActionTypes): OrderState {
    switch(action.type){
        // Requests
        case ORDER_CREATE_REQUEST:
            return { ...state, creating: true };
        case ORDER_ACCEPT_REQUEST:
            return { ...state, accepting: true };
        case ORDER_CONFIRM_REQUEST:
            return { ...state, confirming: true };
        case ORDER_DECLINE_REQUEST:
            return { ...state, declining: true };
        case ORDER_LOAD_LIST_BY_USER_REQUEST:
        case ORDER_LOAD_LIST_BY_EMPLOYEE_REQUEST:
            return { ...state, listLoading: true };

        // Failures
        case ORDER_ACCEPT_FAILURE:
            return { ...state, accepting: false };
        case ORDER_CREATE_FAILURE:
            return { ...state, creating: false };
        case ORDER_CONFIRM_FAILURE:
            return { ...state, confirming: false };
        case ORDER_DECLINE_FAILURE:
            return { ...state, declining: false };
        case ORDER_LOAD_LIST_BY_USER_FAILURE:
        case ORDER_LOAD_LIST_BY_EMPLOYEE_FAILURE:
            return { ...state, listLoading: false };

        // Successes
        case ORDER_CREATE_SUCCESS:
            return { ...state, order: action.order, creating: false };
        case ORDER_LOAD_LIST_BY_USER_SUCCESS:
        case ORDER_LOAD_LIST_BY_EMPLOYEE_SUCCESS:
            return { ...state, orders: action.list, pagesCount: action.pagesCount, totalOrders: action.totalOrders, listLoading: false };
        case ORDER_ACCEPT_SUCCESS:
            return { 
                ...state, accepting: false,
                orders: state.orders && state.orders.map(order => order.id === action.id? {...order, status: 1 } : order ),
            };
        case ORDER_DECLINE_SUCCESS:
            return {
                ...state, declining: false,
                orders: state.orders && state.orders.map(order => order.id === action.id? {...order, status: 2 } : order )
            };
        case ORDER_CONFIRM_SUCCESS:
            return {
                ...state, confirming: false,
                orders: state.orders && state.orders.map(order => order.id === action.id? {...order, status: 3 } : order )
            };
        
        default:
            return state;

    }
}