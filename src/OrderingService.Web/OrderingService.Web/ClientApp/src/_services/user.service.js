import config from '../config';
import { handleResponse } from '../_helpers';

export const userService = {
    login,
    logout,
    signUp,
    updateAuthUser,
    updateAuthEmployeeProfile
};

function login(email, password) {
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ email, password })
    };

    return fetch(`${config.apiUrl}/account/auth`, requestOptions)
        .then(handleResponse)
        .then(user => {
            localStorage.setItem('user', JSON.stringify(user));
            return user;
        });
}

function logout(){
    localStorage.removeItem('user');
}

function updateAuthUser(user){
    let oldUser = JSON.parse(localStorage.getItem('user'));
    user.token = oldUser.token;
    localStorage.setItem('user', JSON.stringify(user));
}

function updateAuthEmployeeProfile(employeeProfile){
    let user = JSON.parse(localStorage.getItem('user'));
    user.employeeProfile = employeeProfile;
    localStorage.setItem('user', JSON.stringify(user));
}

function signUp(user){
    const requestOptions = {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(user)
    };

    return fetch(`${config.apiUrl}/account/sign-up`, requestOptions)
        .then(handleResponse)
        .then(user => {
            localStorage.setItem('user', JSON.stringify(user));
            return user;
        });
}