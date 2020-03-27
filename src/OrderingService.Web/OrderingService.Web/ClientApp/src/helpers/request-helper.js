import { userService } from '../services';

export function handleResponse(response){
    return response.json().then(data => {
        if (!response.ok) {
            if (response.status === 401) {
                userService.logout();
                window.location.reload(true);
            }

            const error = (data && data.errorMessage) || response.statusText;
            return Promise.reject(error);
        }
        return data;
    });
}