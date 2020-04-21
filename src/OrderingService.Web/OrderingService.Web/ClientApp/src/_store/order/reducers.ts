import { 
    OrderState, OrderActionTypes, 
    ORDER_CREATE_REQUEST, ORDER_CREATE_SUCCESS, ORDER_CREATE_FAILURE, 
    ORDER_LOAD_BY_USER_REQUEST, ORDER_LOAD_BY_USER_SUCCESS, ORDER_LOAD_BY_USER_FAILURE, 
    ORDER_LOAD_BY_EMPLOYEE_REQUEST, ORDER_LOAD_BY_EMPLOYEE_SUCCESS, ORDER_LOAD_BY_EMPLOYEE_FAILURE, 
    ORDER_ACCEPT_REQUEST, ORDER_ACCEPT_SUCCESS, ORDER_ACCEPT_FAILURE, 
    ORDER_DECLINE_REQUEST, ORDER_DECLINE_SUCCESS, ORDER_DECLINE_FAILURE, 
    ORDER_CONFIRM_REQUEST, ORDER_CONFIRM_SUCCESS, ORDER_CONFIRM_FAILURE 
} from './types';

const initialState: OrderState = {
    order: undefined,
    orders: undefined,
    pagesCount: undefined,
    totalOrders: undefined
};

export function orderReducer(state: OrderState = initialState, action: OrderActionTypes): OrderState {
    switch(action.type){
        // Requests
        case ORDER_CREATE_REQUEST:
        case ORDER_ACCEPT_REQUEST:
        case ORDER_CONFIRM_REQUEST:
        case ORDER_DECLINE_REQUEST:
        case ORDER_LOAD_BY_USER_REQUEST:
        case ORDER_LOAD_BY_EMPLOYEE_REQUEST:
            return state;

        // Failures
        case ORDER_ACCEPT_FAILURE:
        case ORDER_CREATE_FAILURE:
        case ORDER_CONFIRM_FAILURE:
        case ORDER_DECLINE_FAILURE:
        case ORDER_LOAD_BY_USER_FAILURE:
        case ORDER_LOAD_BY_EMPLOYEE_FAILURE:
            return state;

        // Successes
        case ORDER_CREATE_SUCCESS:
            return { ...state, order: action.order };
        case ORDER_LOAD_BY_USER_SUCCESS:
        case ORDER_LOAD_BY_EMPLOYEE_SUCCESS:
            return { ...state, orders: action.list, pagesCount: action.pagesCount, totalOrders: action.totalOrders };
        case ORDER_ACCEPT_SUCCESS:
            return { 
                ...state,
                orders: state.orders && state.orders.map(order => order.id === action.id? {...order, status: 1 } : order ),
            };
        case ORDER_DECLINE_SUCCESS:
            return {
                ...state,
                orders: state.orders && state.orders.map(order => order.id === action.id? {...order, status: 2 } : order )
            };
        case ORDER_CONFIRM_SUCCESS:
            return {
                ...state,
                orders: state.orders && state.orders.map(order => order.id === action.id? {...order, status: 3 } : order )
            };
        
        default:
            return state;

    }
}