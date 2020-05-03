export const ALERT_SUCCESS = 'alert/success';
export const ALERT_ERROR = 'alert/failure';
export const ALERT_CLEAR = 'alert/clear';

interface AlertSuccessAction {
    type: typeof ALERT_SUCCESS;
    message: string;
};

interface AlertErrorAction {
    type: typeof ALERT_ERROR,
    message: string;
};

interface AlertClearAction {
    type: typeof ALERT_CLEAR;
};

export type AlertActionTypes = AlertSuccessAction | AlertErrorAction | AlertClearAction;

export interface AlertState {
    alerts: Array<{
        type: 'success' | 'danger',
        message: string
    }>;
};