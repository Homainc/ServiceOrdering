import { orderConstants } from '../_constants';
import { orderService } from '../_services';
import { history } from '../_helpers';
import { ThunkAction } from 'redux-thunk';
import { OrderAction, OrderState } from '../_reducers/order.reducer';
import { OrderDTO } from '../WebApiModels';

type OrderThunkResult<R> = ThunkAction<R, OrderState, undefined, OrderAction>;

export const orderActions = {
    createOrder,
    loadOrdersByUser,
    loadOrdersByEmployee,
    acceptOrder,
    declineOrder,
    confirmOrder
};

const defaultAction: OrderAction = {
    id: undefined,
    type: '',
    order: undefined,
    orders: undefined,
    pagesCount: undefined,
    totalOrders: undefined
};

function createOrder(order: OrderDTO): OrderThunkResult<void> {
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

    function request(): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_CREATE_REQUEST 
        }; 
    }
    function success(order: OrderDTO): OrderAction { 
        return {
            ...defaultAction, 
            type: orderConstants.ORDER_CREATE_SUCCESS, order 
        }; 
    }
    function failure(): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_CREATE_FAILURE 
        }; 
    }
}

function loadOrdersByUser(userId: string, pageNumber: number): OrderThunkResult<void> {
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

    function request(): OrderAction { 
        return {
            ...defaultAction, 
            type: orderConstants.ORDER_LOAD_BY_USER_REQUEST 
        }; 
    }
    function success(orders: Array<OrderDTO>, pagesCount: number, totalOrders: number): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_LOAD_BY_USER_SUCCESS, 
            orders, 
            pagesCount, 
            totalOrders 
        }; 
    }
    function failure(): OrderAction { 
        return {
            ...defaultAction, 
            type: orderConstants.ORDER_LOAD_BY_USER_FAILURE 
        }; 
    }
}

function loadOrdersByEmployee(employeeId: string, pageNumber: number): OrderThunkResult<void> {
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

    function request(): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_LOAD_BY_EMPLOYEE_REQUEST 
        }; 
        }
    function success(orders: Array<OrderDTO>, pagesCount: number, totalOrders: number): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_LOAD_BY_EMPLOYEE_SUCCESS, 
            orders, 
            pagesCount, 
            totalOrders 
        }; 
    }
    function failure(): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_LOAD_BY_EMPLOYEE_FAILURE 
        }; 
    }
}

function acceptOrder(orderId: number): OrderThunkResult<void> {
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

    function request(): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_ACCEPT_REQUEST 
        }; 
    }
    function success(id: number): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_ACCEPT_SUCCESS, id 
        }; 
    }
    function failure(): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_ACCEPT_FAILURE 
        }; 
    }
}

function declineOrder(orderId: number): OrderThunkResult<void> {
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

    function request(): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_DECLINE_REQUEST 
        }; 
    }
    function success(id: number): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_DECLINE_SUCCESS, 
            id 
        }; 
    }
    function failure(): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_DECLINE_FAILURE 
        }; 
    }
}

function confirmOrder(orderId: number): OrderThunkResult<void> {
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

    function request(): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_CONFIRM_REQUEST 
        }; 
    }
    function success(id: number): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_CONFIRM_SUCCESS, 
            id 
        }; 
    }
    function failure(): OrderAction { 
        return { 
            ...defaultAction,
            type: orderConstants.ORDER_CONFIRM_FAILURE 
        }; 
    }
}