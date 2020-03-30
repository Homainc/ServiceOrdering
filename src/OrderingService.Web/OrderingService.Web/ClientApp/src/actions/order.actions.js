import { orderConstants } from '../constants';
import { orderService } from '../services';
import { history } from '../helpers';

export const orderActions = {
    createOrder,
    loadOrdersByUser
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

function loadOrdersByUser(user_id) {
    return dispatch => {
        dispatch(request());

        orderService.loadOrdersByUser(user_id)
            .then(data => {
                dispatch(success(data.value));
            },
            error => {
                dispatch(failure());
            });
    };

    function request() { return { type: orderConstants.ORDER_LOAD_BY_USER_REQUEST }; }
    function success(orders) { return { type: orderConstants.ORDER_LOAD_BY_USER_SUCCESS, orders }; }
    function failure() { return { type: orderConstants.ORDER_LOAD_BY_USER_FAILURE }; }
}