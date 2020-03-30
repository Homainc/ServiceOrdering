import { orderConstants } from '../constants';
import { orderService } from '../services';
import { history } from '../helpers';

export const orderActions = {
    createOrder
};

function createOrder(order){
    return dispatch => {
        dispatch(request());

        orderService.createOrder(order)
            .then(order => {
                dispatch(success(order));
                history.push('/profile');
            },
            error => {
                dispatch(failure());
            });
    };

    function request() { return { type: orderConstants.ORDER_CREATE_REQUEST }; }
    function success(order) { return { type: orderConstants.ORDER_CREATE_SUCCESS, order }; }
    function failure() { return { type: orderConstants.ORDER_CREATE_FAILURE }; }
}