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
                orders: action.orders
            };
        case orderConstants.ORDER_LOAD_BY_USER_FAILURE:
            return {};


        default:
            return state;

    }
}