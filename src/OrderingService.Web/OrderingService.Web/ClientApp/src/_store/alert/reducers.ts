import { AlertState, ALERT_SUCCESS, ALERT_ERROR, ALERT_CLEAR, AlertActionTypes } from "./types";

const initialState: AlertState = {
    alerts: []
};

export function alertReducer(state: AlertState = initialState, action: AlertActionTypes): AlertState {
    switch (action.type) {
        case ALERT_SUCCESS:
            state.alerts.push({type: 'success', message: action.message});
            return { ...state };
        case ALERT_ERROR:
            state.alerts.push({type: 'danger', message: action.message});
            return { ...state };
        case ALERT_CLEAR:
            state.alerts.splice(-1);
            return { ...state };
        default:
            return state;
    }
}