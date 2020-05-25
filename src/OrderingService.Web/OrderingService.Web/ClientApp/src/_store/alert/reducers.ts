import { 
    AlertState, AlertActionTypes,
    ALERT_INFO, ALERT_SUCCESS, ALERT_ERROR, 
    ALERT_CLEAR, 
    ALERT_SUCCESS_TITLE,
    ALERT_ERROR_TITLE,
    ALERT_INFO_TITLE
} from "./types";

const initialState: AlertState = {
    alerts: []
};

export function alertReducer(state: AlertState = initialState, action: AlertActionTypes): AlertState {
    switch (action.type) {
        case ALERT_SUCCESS:
            state.alerts.push({type: 'success', message: action.message, title: ALERT_SUCCESS_TITLE});
            return { ...state };
        case ALERT_ERROR:
            state.alerts.push({type: 'danger', message: action.message, title: ALERT_ERROR_TITLE});
            return { ...state };
        case ALERT_INFO:
            state.alerts.push({type: 'info', message: action.message, title: ALERT_INFO_TITLE});
            return { ...state };
        case ALERT_CLEAR:
            state.alerts.splice(0, 1);
            return { ...state };
        default:
            return state;
    }
}