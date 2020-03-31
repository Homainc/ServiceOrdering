import { orderConstants } from '../constants';

export function order(state = {}, action){
    switch(action.type){
        // CREATE ORDER
        case orderConstants.ORDER_CREATE_REQUEST:
            return {
                isOrderCreating: true
            };
        case orderConstants.ORDER_CREATE_SUCCESS:
            return {
                order: action.order
            };
        case orderConstants.ORDER_CREATE_FAILURE:
            return {};

        // LOAD ORDER BY USER
        case orderConstants.ORDER_LOAD_BY_USER_REQUEST:
            return {
                isOrdersLoading: true
            };
        case orderConstants.ORDER_LOAD_BY_USER_SUCCESS:
            return {
                orders: action.orders,
                isOrdersLoading: false
            };
        case orderConstants.ORDER_LOAD_BY_USER_FAILURE:
            return {};

        // LOAD ORDER BY USER
        case orderConstants.ORDER_LOAD_BY_EMPLOYEE_REQUEST:
            return {
                isOrdersLoading: true
            };
        case orderConstants.ORDER_LOAD_BY_EMPLOYEE_SUCCESS:
            return {
                isOrdersLoading: false,
                orders: action.orders
            };
        case orderConstants.ORDER_LOAD_BY_EMPLOYEE_FAILURE:
            return {};

        // ORDER ACCEPT
        case orderConstants.ORDER_ACCEPT_REQUEST:
            return {
                isOrderAccepting: true,
                orders: state.orders,
            };
        case orderConstants.ORDER_ACCEPT_SUCCESS:
            return {
                isOrderAccepting: false,
                orders: state.orders.map(order => order.id === action.id? {...order, status: 1 } : order )
            };
        case orderConstants.ORDER_ACCEPT_FAILURE:
            return {};

        // ORDER DECLINE
        case orderConstants.ORDER_DECLINE_REQUEST:
            return {
                isOrderDeclining: true,
                orders: state.orders
            };
        case orderConstants.ORDER_DECLINE_SUCCESS:
            return {
                isOrderDeclining: false,
                orders: state.orders.map(order => order.id === action.id? {...order, status: 2 } : order)
            };
        case orderConstants.ORDER_DECLINE_FAILURE:
            return {};

        // ORDER CONFIRM
        case orderConstants.ORDER_CONFIRM_REQUEST:
            return {
                isOrderConfirming: true,
                orders: state.orders
            };
        case orderConstants.ORDER_CONFIRM_SUCCESS:
            return {
                isOrderConfirming: false,
                orders: state.orders.map(order => order.id === action.id? {...order, status: 3 } : order )
            };
        case orderConstants.ORDER_CONFIRM_FAILURE:
            return {};
        
        default:
            return state;

    }
}