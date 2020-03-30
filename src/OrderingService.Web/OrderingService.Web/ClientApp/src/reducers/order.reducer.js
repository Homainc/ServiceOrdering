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

        default:
            return state;

    }
}