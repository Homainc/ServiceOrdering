import Api from '../WebApiModels';
import { logOut } from '../_store/auth/actions';

function httpRequestErrorHandler(error: any){
    if (error.status === 401) {
        logOut();
        window.location.reload(true);
    }
}

export function getErrorMessageFromEx(error: any): string {
    const response = error.response;
    return response.body && (response.body.errorMessage || response.body.errors);
}

const api = new Api();

api.addErrorHandler(httpRequestErrorHandler);

export { api };