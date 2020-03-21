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

function updateProfile(){

}