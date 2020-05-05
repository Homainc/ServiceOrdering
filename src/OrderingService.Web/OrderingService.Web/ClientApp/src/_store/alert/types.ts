export const ALERT_SUCCESS_TITLE = 'Success';
export const ALERT_ERROR_TITLE = 'Error occured';
export const ALERT_INFO_TITLE = 'Notice';

export const ALERT_SUCCESS = 'alert/success';
export const ALERT_ERROR = 'alert/failure';
export const ALERT_CLEAR = 'alert/clear';
export const ALERT_INFO = 'alert/info';

interface AlertMessageAction {
    type: typeof ALERT_SUCCESS | typeof ALERT_ERROR | typeof ALERT_INFO;
    message: string;
};

interface AlertClearAction {
    type: typeof ALERT_CLEAR;
};

export type AlertActionTypes = AlertMessageAction | AlertClearAction;

export interface AlertState {
    alerts: Array<{
        type: 'success' | 'danger' | 'info',
        title: string,
        message: string
    }>;
};