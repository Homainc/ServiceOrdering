import { history, api, getErrorMessageFromEx } from '../../_helpers';
import { ThunkAction } from 'redux-thunk';
import { OrderCreateDto, OrderDto, IPagedResultOfOrderDto, ValidationProblemDetails } from '../../WebApiModels';
import { 
    OrderState, OrderActionTypes, 
    ORDER_CREATE_REQUEST, ORDER_CREATE_SUCCESS, ORDER_CREATE_FAILURE, 
    ORDER_LOAD_LIST_BY_USER_REQUEST, ORDER_LOAD_LIST_BY_USER_SUCCESS, ORDER_LOAD_LIST_BY_USER_FAILURE, 
    ORDER_LOAD_LIST_BY_EMPLOYEE_REQUEST, ORDER_LOAD_LIST_BY_EMPLOYEE_SUCCESS, ORDER_LOAD_LIST_BY_EMPLOYEE_FAILURE, 
    ORDER_ACCEPT_REQUEST, ORDER_ACCEPT_SUCCESS, ORDER_ACCEPT_FAILURE, 
    ORDER_DECLINE_REQUEST, ORDER_DECLINE_SUCCESS, ORDER_DECLINE_FAILURE, 
    ORDER_CONFIRM_REQUEST, ORDER_CONFIRM_SUCCESS, ORDER_CONFIRM_FAILURE 
} from './types';
import { RootState } from '../';

export function create(
    order: OrderCreateDto
): ThunkAction<void, RootState, undefined, OrderActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            const createdOrder = (await api.Order_Create({ orderDto: order })).body as OrderDto;
            
            dispatch(success(createdOrder));
            history.push('/profile');
        }
        catch (err) {
            const errObj = err.response.body as ValidationProblemDetails;
            dispatch(failure(errObj.title || ''));
            throw errObj.errors;
        }
    };

    function request(): OrderActionTypes { 
        return { type: ORDER_CREATE_REQUEST }; 
    }
    function success(order: OrderDto): OrderActionTypes { 
        return { type: ORDER_CREATE_SUCCESS, order }; 
    }
    function failure(error: string): OrderActionTypes { 
        return { type: ORDER_CREATE_FAILURE, error }; 
    }
}

export function loadOrdersByUser(
    userId: string, 
    pageNumber: number
): ThunkAction<void, OrderState, undefined, OrderActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            const pagedResult = (await api.Order_GetUserOrders({ id: userId, pageNumber })).body as IPagedResultOfOrderDto;
            
            dispatch(success(pagedResult));
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
        }
    };

    function request(): OrderActionTypes { 
        return { type: ORDER_LOAD_LIST_BY_USER_REQUEST }; 
    }
    function success(pagedResult: IPagedResultOfOrderDto): OrderActionTypes { 
        return {
            type: ORDER_LOAD_LIST_BY_USER_SUCCESS, 
            list: pagedResult.value as OrderDto[], 
            pagesCount: pagedResult.pagesCount, 
            totalOrders: pagedResult.total 
        }; 
    }
    function failure(error: string): OrderActionTypes { 
        return { type: ORDER_LOAD_LIST_BY_USER_FAILURE, error }; 
    }
}

export function loadOrdersByEmployee(
    employeeId: string, 
    pageNumber: number
): ThunkAction<void, OrderState, undefined, OrderActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            const pagedResult = (await api.Order_GetEmployeeOrders({ id: employeeId, pageNumber })).body as IPagedResultOfOrderDto;
            
            dispatch(success(pagedResult));
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
        }
    };

    function request(): OrderActionTypes { 
        return { type: ORDER_LOAD_LIST_BY_EMPLOYEE_REQUEST }; 
    }
    function success(pagedResult: IPagedResultOfOrderDto): OrderActionTypes { 
        return { 
            type: ORDER_LOAD_LIST_BY_EMPLOYEE_SUCCESS, 
            list: pagedResult.value as OrderDto[], 
            pagesCount: pagedResult.pagesCount, 
            totalOrders: pagedResult.total 
        }; 
    }
    function failure(error: string): OrderActionTypes { 
        return { type: ORDER_LOAD_LIST_BY_EMPLOYEE_FAILURE, error }; 
    }
}

export function accept(
    orderId: number
): ThunkAction<void, OrderState, undefined, OrderActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            await api.Order_Take({ id: orderId });
            
            dispatch(success(orderId));
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
        }
    };

    function request(): OrderActionTypes { 
        return { type: ORDER_ACCEPT_REQUEST }; 
    }
    function success(id: number): OrderActionTypes { 
        return { type: ORDER_ACCEPT_SUCCESS, id }; 
    }
    function failure(error: string): OrderActionTypes { 
        return { type: ORDER_ACCEPT_FAILURE, error }; 
    }
}

export function decline(
    orderId: number
): ThunkAction<void, OrderState, undefined, OrderActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            await api.Order_Decline({ id: orderId });

            dispatch(success(orderId));
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
        }
    };

    function request(): OrderActionTypes { 
        return { type: ORDER_DECLINE_REQUEST }; 
    }
    function success(id: number): OrderActionTypes { 
        return { type: ORDER_DECLINE_SUCCESS, id }; 
    }
    function failure(error: string): OrderActionTypes { 
        return { type: ORDER_DECLINE_FAILURE, error }; 
    }
}

export function confirm(
    orderId: number
): ThunkAction<void, RootState, undefined, OrderActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            await api.Order_ConfirmCompletion({ id: orderId });

            dispatch(success(orderId));
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
        }
    };

    function request(): OrderActionTypes { 
        return { type: ORDER_CONFIRM_REQUEST }; 
    }
    function success(id: number): OrderActionTypes { 
        return { type: ORDER_CONFIRM_SUCCESS, id }; 
    }
    function failure(error: string): OrderActionTypes { 
        return { type: ORDER_CONFIRM_FAILURE, error }; 
    }
}