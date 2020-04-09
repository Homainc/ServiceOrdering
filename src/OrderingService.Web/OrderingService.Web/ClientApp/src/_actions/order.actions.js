import { orderConstants } from '../_constants';
import { orderService } from '../_services';
import { history } from '../_helpers';

export const orderActions = {
    createOrder,
    loadOrdersByUser,
    loadOrdersByEmployee,
    acceptOrder,
    declineOrder,
    confirmOrder
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

function loadOrdersByUser(userId, pageNumber) {
    return dispatch => {
        dispatch(request());

        orderService.loadOrdersByUser(userId, pageNumber)
            .then(data => {
                dispatch(success(data.value, data.pagesCount, data.total));
            },
            error => {
                dispatch(failure());
            });
    };

    function request() { return { type: orderConstants.ORDER_LOAD_BY_USER_REQUEST }; }
    function success(orders, pagesCount, totalOrders) { return { type: orderConstants.ORDER_LOAD_BY_USER_SUCCESS, orders, pagesCount, totalOrders }; }
    function failure() { return { type: orderConstants.ORDER_LOAD_BY_USER_FAILURE }; }
}

function loadOrdersByEmployee(employeeId, pageNumber) {
    return dispatch => {
        dispatch(request());
        orderService.loadOrdersByEmployee(employeeId, pageNumber)
            .then(data => {
                dispatch(success(data.value, data.pagesCount, data.total));
            },
            error => {
                dispatch(failure());
            });
    };

    function request() { return { type: orderConstants.ORDER_LOAD_BY_EMPLOYEE_REQUEST }; }
    function success(orders, pagesCount, totalOrders) { return { type: orderConstants.ORDER_LOAD_BY_EMPLOYEE_SUCCESS, orders, pagesCount, totalOrders }; }
    function failure() { return { type: orderConstants.ORDER_LOAD_BY_EMPLOYEE_FAILURE }; }
}

function acceptOrder(orderId) {
    return dispatch => {
        dispatch(request());
        orderService.acceptOrder(orderId)
            .then(data => {
                dispatch(success(orderId));
            },
            error => {
                dispatch(failure());
            });
    };

    function request() { return { type: orderConstants.ORDER_ACCEPT_REQUEST }; }
    function success(id) { return { type: orderConstants.ORDER_ACCEPT_SUCCESS, id }; }
    function failure() { return { type: orderConstants.ORDER_ACCEPT_FAILURE }; }
}

function declineOrder(orderId) {
    return dispatch => {
        dispatch(request());
        orderService.declineOrder(orderId)
            .then(data => {
                dispatch(success(orderId));
            },
            error => {
                dispatch(failure());
            });
    };

    function request() { return { type: orderConstants.ORDER_DECLINE_REQUEST }; }
    function success(id) { return { type: orderConstants.ORDER_DECLINE_SUCCESS, id }; }
    function failure() { return { type: orderConstants.ORDER_DECLINE_FAILURE }; }
}

function confirmOrder(orderId) {
    return dispatch => {
        dispatch(request());
        orderService.confirmOrder(orderId)
            .then(data => {
                dispatch(success(orderId));
            },
            error => {
                dispatch(failure());
            });
    };

    function request() { return { type: orderConstants.ORDER_CONFIRM_REQUEST }; }
    function success(id) { return { type: orderConstants.ORDER_CONFIRM_SUCCESS, id }; }
    function failure() { return { type: orderConstants.ORDER_CONFIRM_FAILURE }; }
}