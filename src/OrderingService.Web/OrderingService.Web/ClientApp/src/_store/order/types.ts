import { OrderDto } from "../../WebApiModels";

export const ORDER_CREATE_REQUEST = 'order/create (request)';
export const ORDER_CREATE_SUCCESS = 'order/create (success)';
export const ORDER_CREATE_FAILURE = 'order/create (failure)';

export const ORDER_LOAD_LIST_BY_USER_REQUEST = 'order/load_list_by_user (request)';
export const ORDER_LOAD_LIST_BY_USER_SUCCESS = 'order/load_list_by_user (success)';
export const ORDER_LOAD_LIST_BY_USER_FAILURE = 'order/load_list_by_user (failure)';
    
export const ORDER_LOAD_LIST_BY_EMPLOYEE_REQUEST = 'order/load_list_by_employee (request)';
export const ORDER_LOAD_LIST_BY_EMPLOYEE_SUCCESS = 'order/load_list_by_employee (success)';
export const ORDER_LOAD_LIST_BY_EMPLOYEE_FAILURE ='order/load_list_by_employee (failure)';

export const ORDER_ACCEPT_REQUEST = 'order/accept (request)';
export const ORDER_ACCEPT_SUCCESS = 'order/accept (success)';
export const ORDER_ACCEPT_FAILURE = 'order/accept (failure)';

export const ORDER_DECLINE_REQUEST = 'order/decline (request)';
export const ORDER_DECLINE_SUCCESS = 'order/decline (success)';
export const ORDER_DECLINE_FAILURE = 'order/decline (failure)';

export const ORDER_CONFIRM_REQUEST = 'order/confirm (request)';
export const ORDER_CONFIRM_SUCCESS = 'order/confirm (success)';
export const ORDER_CONFIRM_FAILURE = 'order/confirm (failure)';

export const ORDER_STATUS = ['Waiting for the employee', 'In progress', 'Declined', 'Done'];

interface OrderRequestAction {
    type: 
        typeof ORDER_CREATE_REQUEST | typeof ORDER_LOAD_LIST_BY_USER_REQUEST |
        typeof ORDER_LOAD_LIST_BY_EMPLOYEE_REQUEST | typeof ORDER_ACCEPT_REQUEST |
        typeof ORDER_DECLINE_REQUEST | typeof ORDER_CONFIRM_REQUEST;
};

interface OrderCreateSuccessAction {
    type: typeof ORDER_CREATE_SUCCESS;
    order: OrderDto;
};

interface OrderLoadSuccessAction {
    type: typeof ORDER_LOAD_LIST_BY_USER_SUCCESS | typeof ORDER_LOAD_LIST_BY_EMPLOYEE_SUCCESS;
    list: Array<OrderDto>;
    pagesCount: number;
    totalOrders: number;
};

interface OrderChangeStateSuccessAction {
    type: typeof ORDER_ACCEPT_SUCCESS | typeof ORDER_CONFIRM_SUCCESS | typeof ORDER_DECLINE_SUCCESS;
    id: number;
};

interface OrderFailureAction {
    type:
        typeof ORDER_CREATE_FAILURE | typeof ORDER_LOAD_LIST_BY_USER_FAILURE |
        typeof ORDER_LOAD_LIST_BY_EMPLOYEE_FAILURE | typeof ORDER_ACCEPT_FAILURE |
        typeof ORDER_DECLINE_FAILURE | typeof ORDER_CONFIRM_FAILURE;
    error: string;
};

export type OrderActionTypes = 
    OrderRequestAction |
    OrderLoadSuccessAction |
    OrderCreateSuccessAction |
    OrderChangeStateSuccessAction |
    OrderFailureAction;

export interface OrderState {
    listLoading: boolean;
    creating: boolean;
    accepting: boolean;
    declining: boolean;
    confirming: boolean;
    order?: OrderDto;
    orders?: Array<OrderDto>;
    pagesCount?: number;
    totalOrders?: number;
};
    