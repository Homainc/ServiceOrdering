import { orderConstants } from '../_constants';
import { OrderDTO } from '../WebApiModels';

type OrderState = {
    isOrderConfirming: boolean;
    isOrderDeclining: boolean;
    isOrdersLoading: boolean;
    isOrderCreating: boolean;
    isOrderAccepting: boolean;
    order: OrderDTO | undefined;
    orders: Array<OrderDTO> | undefined;
    pagesCount: number | undefined;
    totalOrders: number | undefined;
};

const initialState: OrderState = {
    isOrderConfirming: false,
    isOrderDeclining: false,
    isOrdersLoading: false,
    isOrderCreating: false,
    isOrderAccepting: false,
    order: undefined,
    orders: undefined,
    pagesCount: undefined,
    totalOrders: undefined
};

type OrderAction = {
    id: number | undefined;
    type: string;
    order: OrderDTO | undefined;
    orders: Array<OrderDTO> | undefined;
    pagesCount: number | undefined;
    totalOrders: number | undefined;
};

export function order(state: OrderState = initialState, action: OrderAction): OrderState {
    switch(action.type){
        // CREATE ORDER
        case orderConstants.ORDER_CREATE_REQUEST:
            return {
                ...state,
                isOrderCreating: true,
                order: action.order

            };
        case orderConstants.ORDER_CREATE_SUCCESS:
            return {
                ...state,
                isOrderCreating: false,
                order: action.order
            };
        case orderConstants.ORDER_CREATE_FAILURE:
            return {
                ...state,
                isOrderCreating: false,
                order: undefined
            };

        // LOAD ORDER BY USER
        case orderConstants.ORDER_LOAD_BY_USER_REQUEST:
            return {
                ...state,
                isOrdersLoading: true,
                orders: undefined,
                pagesCount: undefined,
                totalOrders: undefined
            };
        case orderConstants.ORDER_LOAD_BY_USER_SUCCESS:
            return {
                ...state,
                orders: action.orders,
                isOrdersLoading: false,
                pagesCount: action.pagesCount,
                totalOrders: action.totalOrders
            };
        case orderConstants.ORDER_LOAD_BY_USER_FAILURE:
            return {
                ...state,
                isOrdersLoading: false,
            };

        // LOAD ORDER BY USER
        case orderConstants.ORDER_LOAD_BY_EMPLOYEE_REQUEST:
            return {
                ...state,
                isOrdersLoading: true,
                orders: undefined,
                pagesCount: undefined,
                totalOrders: undefined
            };
        case orderConstants.ORDER_LOAD_BY_EMPLOYEE_SUCCESS:
            return {
                ...state,
                isOrdersLoading: false,
                orders: action.orders,
                pagesCount: action.pagesCount,
                totalOrders: action.totalOrders
            };
        case orderConstants.ORDER_LOAD_BY_EMPLOYEE_FAILURE:
            return {
                ...state,
                isOrdersLoading: false
            };

        // ORDER ACCEPT
        case orderConstants.ORDER_ACCEPT_REQUEST:
            return {
                ...state,
                isOrderAccepting: true,
            };
        case orderConstants.ORDER_ACCEPT_SUCCESS:
            return {
                ...state,
                isOrderAccepting: false,
                orders: state.orders && state.orders.map(order => order.id === action.id? {...order, status: 1 } : order ),
            };
        case orderConstants.ORDER_ACCEPT_FAILURE:
            return {
                ...state,
                isOrderAccepting: false
            };

        // ORDER DECLINE
        case orderConstants.ORDER_DECLINE_REQUEST:
            return {
                ...state,
                isOrderDeclining: true,
            };
        case orderConstants.ORDER_DECLINE_SUCCESS:
            return {
                ...state,
                isOrderDeclining: false,
                orders: state.orders && state.orders.map(order => order.id === action.id? {...order, status: 2 } : order )
            };
        case orderConstants.ORDER_DECLINE_FAILURE:
            return {
                ...state,
                isOrderDeclining: false
            };

        // ORDER CONFIRM
        case orderConstants.ORDER_CONFIRM_REQUEST:
            return {
                ...state,
                isOrderConfirming: true
            };
        case orderConstants.ORDER_CONFIRM_SUCCESS:
            return {
                ...state,
                isOrderConfirming: false,
                orders: state.orders && state.orders.map(order => order.id === action.id? {...order, status: 3 } : order )
            };
        case orderConstants.ORDER_CONFIRM_FAILURE:
            return {
                ...state,
                isOrderConfirming: false
            };
        
        default:
            return state;

    }
}