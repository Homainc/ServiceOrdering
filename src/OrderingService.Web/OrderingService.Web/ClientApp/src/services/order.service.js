import config from '../config';
import { handleResponse, authHeader } from '../helpers';

export const orderService = {
    createOrder
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