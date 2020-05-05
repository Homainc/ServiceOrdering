import { AlertActionTypes, ALERT_SUCCESS, ALERT_ERROR, ALERT_CLEAR, ALERT_INFO } from "./types";
import { ThunkAction } from "redux-thunk";
import { RootState } from "..";

export function success(
    message: string
): ThunkAction<void, RootState, undefined, AlertActionTypes> {
    return dispatch => {
        dispatch({ type: ALERT_SUCCESS, message });
        
        setTimeout(() => {
            dispatch(clear());
        }, 5000);
    };
}

export function error(
    message: string
): ThunkAction<void, RootState, undefined, AlertActionTypes> {
    return dispatch => {
        dispatch({ type: ALERT_ERROR, message });

        setTimeout(() => {
            dispatch(clear());
        }, 5000);
    };
}

export function info(
    message: string
): ThunkAction<void, RootState, undefined, AlertActionTypes> {
    return dispatch => {
        dispatch({ type: ALERT_INFO, message });

        setTimeout(() => {
            dispatch(clear());
        }, 5000);
    };
};

export function clear(): AlertActionTypes {
    return { type: ALERT_CLEAR };
}