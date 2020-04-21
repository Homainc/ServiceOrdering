import { AlertState, ALERT_SUCCESS, ALERT_ERROR, ALERT_CLEAR, AlertActionTypes } from "./types";

const initialState: AlertState = {
    type: undefined,
    message: undefined
};

export function alertReducer(state: AlertState = initialState, action: AlertActionTypes): AlertState {
    switch (action.type) {
        case ALERT_SUCCESS:
            return {
                type: 'success',
                message: action.message
            };
        case ALERT_ERROR:
            return {
                type: 'danger',
                message: action.message
            };
        case ALERT_CLEAR:
            return { 
                type: undefined, 
                message: undefined 
            };
        default:
            return state;
    }
}