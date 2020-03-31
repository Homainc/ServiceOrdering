import config from '../config';
import { handleResponse, authHeader } from '../helpers';

export const orderService = {
    createOrder,
    loadOrdersByUser,
    loadOrdersByEmployee,
    acceptOrder,
    declineOrder,
    confirmOrder
};

function createOrder(order) {
    const requestOptions = {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type' : 'application/json',
        },
        body: JSON.stringify(order)
    };
    console.log(JSON.stringify(order));
    return fetch(`${config.apiUrl}/order`, requestOptions)
        .then(handleResponse);
}

function loadOrdersByUser(userId){
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${config.apiUrl}/order/user/${userId}`, requestOptions)
       .then(handleResponse);
}

function loadOrdersByEmployee(employeeId) {
    const requestOptions = {
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${config.apiUrl}/order/employee/${employeeId}`, requestOptions)
        .then(handleResponse);
}

function acceptOrder(orderId) {
    const requestOptions = {
        method: 'PUT',
        headers: authHeader()
    };
    return fetch(`${config.apiUrl}/order/take/${orderId}`, requestOptions)
        .then(handleResponse);
}

function declineOrder(orderId) {
    const requestOptions = {
        method: 'PUT',
        headers: authHeader()
    };
    return fetch(`${config.apiUrl}/order/decline/${orderId}`, requestOptions)
        .then(handleResponse);
}

function confirmOrder(orderId) {
    const requestOptions = {
        method: 'PUT',
        headers: authHeader()
    };
    return fetch(`${config.apiUrl}/order/confirm/${orderId}`, requestOptions)
        .then(handleResponse);
}