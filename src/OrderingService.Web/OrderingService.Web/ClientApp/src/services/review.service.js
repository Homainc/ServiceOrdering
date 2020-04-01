import { authHeader, handleResponse } from "../helpers";
import config from "../config";

export const reviewService = {
    createReview,
    getReviewsByEmployee
};

function createReview(review) {
    const requestOptions = {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(review)
    };
    return fetch(`${config.apiUrl}/review`, requestOptions)
        .then(handleResponse);
}

function getReviewsByEmployee(employeeId) {
    const requestOptions = {
        method: 'GET',
    };
    return fetch(`${config.apiUrl}/review/${employeeId}`, requestOptions)
        .then(handleResponse);
}