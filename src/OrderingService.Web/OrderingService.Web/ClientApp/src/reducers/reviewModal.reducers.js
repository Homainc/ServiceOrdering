import { reviewModalConstants } from "../constants";

export function reviewModal(state = { isModalOpened: false }, action){
    switch(action.type){
        case reviewModalConstants.REVIEW_MODAL_SHOW:
            return {
                isModalOpened: true,
                order: action.order
            };
        case reviewModalConstants.REVIEW_MODAL_CLOSE:
            return {
                isModalOpened: false
            };
        default:
            return state;
    }
}