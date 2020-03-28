import config from '../config';
import { handleResponse, authHeader } from "../helpers";

export const employeeService = {
    createEmployeeProfile,
    updateEmployeeProfile,
    deleteEmployeeProfile,
    loadEmployees,
    loadEmployeeProfile
};

function createEmployeeProfile(profile){
    const requestOptions = {
        method: 'POST',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(profile)
    };
    return fetch(`${config.apiUrl}/employeeProfile`, requestOptions)
        .then(handleResponse);
}

function updateEmployeeProfile(profile){
    const requestOptions = {
        method: 'PUT',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(profile)
    };
    return fetch(`${config.apiUrl}/employeeProfile`, requestOptions)
        .then(handleResponse);
}

function deleteEmployeeProfile(id){
    const requestOptions = {
        method: 'DELETE',
        headers: authHeader()
    };
    return fetch(`${config.apiUrl}/employeeProfile/?id=${id}`, requestOptions)
        .then(handleResponse);
}

function loadEmployees(pageNumber){
    const requestOptions = {
        method: 'GET',
    };
    return fetch(`${config.apiUrl}/employeeProfile?pageNumber=${pageNumber}`, requestOptions)
        .then(handleResponse);
}

function loadEmployeeProfile(id){
    const requestOptions = {
        method: 'GET'
    };
    return fetch(`${config.apiUrl}/employeeProfile/${id}`, requestOptions)
        .then(handleResponse);
}