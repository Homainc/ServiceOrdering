import { 
    ReviewModalState, ReviewModalActionTypes, 
    REVIEW_MODAL_SHOW, REVIEW_MODAL_HIDE 
} from "./types";

const initialState: ReviewModalState = {
    modalOpened: false,
    order: undefined
};

export function reviewModalReducer(state: ReviewModalState = initialState, action: ReviewModalActionTypes): ReviewModalState {
    switch(action.type){
        case REVIEW_MODAL_SHOW:
            return { modalOpened: true, order: action.order };
        case REVIEW_MODAL_HIDE:
            return { ...state, modalOpened: false };
        
        default:
            return state;
    }
}