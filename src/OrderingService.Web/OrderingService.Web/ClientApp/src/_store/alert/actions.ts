import { AlertActionTypes, ALERT_SUCCESS, ALERT_ERROR, ALERT_CLEAR } from "./types";

export function success(message: string): AlertActionTypes {
    return { type: ALERT_SUCCESS, message };
}

export function error(message: string): AlertActionTypes {
    return { type: ALERT_ERROR, message };
}

export function clear(): AlertActionTypes {
    return { type: ALERT_CLEAR };
}