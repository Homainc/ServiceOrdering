import { reviewConstants } from "../_constants";
import { ReviewDTO } from "../WebApiModels";

export type ReviewState = {
    isReviewCreating: boolean;
    isReviewsLoading: boolean;
    reviews: Array<ReviewDTO> | undefined;
};

export type ReviewAction = {
    type: string;
    reviews: Array<ReviewDTO> | undefined;
};

const initialState: ReviewState = {
    isReviewCreating: false,
    isReviewsLoading: false,
    reviews: undefined
};

export function review(state: ReviewState = initialState, action: ReviewAction): ReviewState {
    switch(action.type){
        // CREATE REVIEW
        case reviewConstants.REVIEW_CREATE_REQUEST:
            return {
                ...state,
                isReviewCreating: true
            };
        case reviewConstants.REVIEW_CREATE_SUCCESS:
            return {
                ...state,
                isReviewCreating: false
            };
        case reviewConstants.REVIEW_CREATE_FAILURE:
            return {
                ...state,
                isReviewCreating: false
            };

        // GET REVIEW BY EMPLOYEE
        case reviewConstants.REVIEW_GET_BY_EMPLOYEE_REQUEST:
            return {
                ...state,
                isReviewsLoading: true,
                reviews: undefined
            };
        case reviewConstants.REVIEW_GET_BY_EMPLOYEE_SUCCESS:
            return {
                ...state,
                isReviewsLoading: false,
                reviews: action.reviews 
            };
        case reviewConstants.REVIEW_GET_BY_EMPLOYEE_FAILURE:
            return {
                ...state,
                isReviewsLoading: false
            };

        default:
            return state;
    }
}