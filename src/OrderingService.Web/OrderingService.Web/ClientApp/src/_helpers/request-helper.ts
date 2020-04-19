import { userService } from '../_services';
import Api, { ResponseWithBody, ProblemDetails } from '../WebApiModels';
import { alertActions } from '../_actions';

export const api = new Api(); 

export function handleResponse(response: any) {
    console.log(response);
    return response.json().then((data: any) => {
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

export function handleResponseErrors(resp: ResponseWithBody<401, ProblemDetails>) : void {
    console.log(resp);
    if(!resp.ok){
        if(resp.status === 401){
            userService.logout();
            window.location.reload(true);
        }

        const error = resp.body && (resp.body.errorMessage || resp.body.errors || resp.status);
        alertActions.error(error);
    }
}