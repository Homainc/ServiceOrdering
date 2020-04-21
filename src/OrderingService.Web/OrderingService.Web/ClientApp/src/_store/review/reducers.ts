import { 
    ReviewState, ReviewActionTypes, 
    REVIEW_CREATE_REQUEST, REVIEW_CREATE_SUCCESS, REVIEW_CREATE_FAILURE, 
    REVIEW_LOAD_LIST_BY_EMPLOYEE_REQUEST, REVIEW_LOAD_LIST_BY_EMPLOYEE_SUCCESS, REVIEW_LOAD_LIST_BY_EMPLOYEE_FAILURE 
} from "./types";

const initialState: ReviewState = {
    creating: false,
    reviews: undefined
};

export function reviewReducer(state: ReviewState = initialState, action: ReviewActionTypes): ReviewState {
    switch(action.type){
        // Requests
        case REVIEW_CREATE_REQUEST:
            return { ...state, creating: true };
        case REVIEW_LOAD_LIST_BY_EMPLOYEE_REQUEST:
            return state;

        // Failures
        case REVIEW_CREATE_FAILURE:
            return { ...state, creating: false };
        case REVIEW_LOAD_LIST_BY_EMPLOYEE_FAILURE:
            return state;

        // Successes
        case REVIEW_CREATE_SUCCESS:
            return { ...state, creating: false };
        case REVIEW_LOAD_LIST_BY_EMPLOYEE_SUCCESS:
            return { ...state, reviews: action.list };

        default:
            return state;
    }
}