import { alertConstants } from '../_constants';

type AlertState = {
    type: 'success' | 'danger' | undefined;
    message: string | undefined;
};

export type AlertAction = { 
    type: string; 
    message: string | undefined;  
};

export function alert(state: AlertState = { type: undefined, message: undefined }, action: AlertAction): AlertState {
    switch (action.type) {
        case alertConstants.SUCCESS:
            return {
                type: 'success',
                message: action.message
            };
        case alertConstants.ERROR:
            return {
                type: 'danger',
                message: action.message
            };
        case alertConstants.CLEAR:
            return { 
                type: undefined, 
                message: undefined 
            };
        default:
            return state;
    }
}