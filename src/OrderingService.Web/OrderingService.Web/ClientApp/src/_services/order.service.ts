import { api } from '../_helpers';
import { OrderDTO } from '../WebApiModels';

export const orderService = {
    createOrder,
    loadOrdersByUser,
    loadOrdersByEmployee,
    acceptOrder,
    declineOrder,
    confirmOrder
};

async function createOrder(order: OrderDTO): Promise<OrderDTO> {
    const resp = await api.Order_Create({ orderDto: order });
    return resp.body as OrderDTO;
}

async function loadOrdersByUser(userId: string, pageNumber: number): Promise<any> {
    const resp = await api.Order_GetUserOrders({ id: userId, pageNumber });
    return resp.body;
}

async function loadOrdersByEmployee(employeeId: string, pageNumber: number): Promise<any> {
    const resp = await api.Order_GetEmployeeOrders({ id: employeeId, pageNumber });
    return resp.body;
}

async function acceptOrder(orderId: number): Promise<OrderDTO> {
    const resp = await api.Order_Take({ id: orderId });
    return resp.body as OrderDTO;
}

async function declineOrder(orderId: number): Promise<OrderDTO> {
    const resp = await api.Order_Decline({ id: orderId });
    return resp.body as OrderDTO;
}

async function confirmOrder(orderId: number): Promise<OrderDTO> {
    const resp = await api.Order_ConfirmCompletion({ id: orderId });
    return resp.body as OrderDTO;
}