import { alertConstants } from '../_constants';
import { AlertAction } from '../_reducers/alert.reducer';

export const alertActions = {
    success,
    error,
    clear
};

function success(message: string): AlertAction {
    return { type: alertConstants.SUCCESS, message };
}

function error(message: string): AlertAction {
    return { type: alertConstants.ERROR, message };
}

function clear(): AlertAction {
    return { type: alertConstants.CLEAR, message: undefined };
}