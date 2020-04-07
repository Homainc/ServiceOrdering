import { userService } from '../_services';

export function handleResponse(response){
    console.log(response);
    return response.json().then(data => {
        if (!response.ok) {
            if (response.status === 401) {
                userService.logout();
                window.location.reload(true);
            }

            const error = (data && data.errorMessage) || data.errors || response.statusText;
            return Promise.reject(error);
        }
        return data;
    });
}