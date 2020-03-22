import config from "../config";
import { handleResponse, authHeader } from "../helpers";

export const profileService = {
    loadProfile,
    updateProfile
};

function loadProfile(){
    const requestOptions = { 
        method: 'GET',
        headers: authHeader()
    };
    return fetch(`${config.apiUrl}/account/profile`, requestOptions)
        .then(handleResponse);
}

function updateProfile(profile){
    const requestOptions = {
        method: 'PUT',
        headers: {
            ...authHeader(),
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(profile)
    };
    return fetch(`${config.apiUrl}/account`, requestOptions)
        .then(handleResponse)
}