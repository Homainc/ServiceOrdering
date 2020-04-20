import { reviewModalConstants } from "../_constants";
import { OrderDTO } from "../WebApiModels";

export type ReviewModalState = {
    isModalOpened: boolean;
    order: OrderDTO | undefined;
};

export type ReviewModalAction = {
    type: string;
    order: OrderDTO | undefined;
};

const initialState: ReviewModalState = {
    isModalOpened: false,
    order: undefined
};

export function reviewModal(state: ReviewModalState = initialState, action: ReviewModalAction): ReviewModalState {
    switch(action.type){
        case reviewModalConstants.REVIEW_MODAL_SHOW:
            return {
                ...state,
                isModalOpened: true,
                order: action.order
            };
        case reviewModalConstants.REVIEW_MODAL_CLOSE:
            return {
                ...state,
                isModalOpened: false
            };
        default:
            return state;
    }
}