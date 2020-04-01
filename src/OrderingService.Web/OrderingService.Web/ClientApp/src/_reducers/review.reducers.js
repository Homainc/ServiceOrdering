import { reviewConstants } from "../_constants";

export function review(state = {}, action) {
    switch(action.type){
        // CREATE REVIEW
        case reviewConstants.REVIEW_CREATE_REQUEST:
            return {
                isReviewCreating: true
            };
        case reviewConstants.REVIEW_CREATE_SUCCESS:
            return {
                isReviewCreating: false
            };
        case reviewConstants.REVIEW_CREATE_FAILURE:
            return {};

        // GET REVIEW BY EMPLOYEE
        case reviewConstants.REVIEW_GET_BY_EMPLOYEE_REQUEST:
            return {
                isReviewsLoading: true
            };
        case reviewConstants.REVIEW_GET_BY_EMPLOYEE_SUCCESS:
            return {
                isReviewsLoading: false,
                reviews: action.reviews 
            };
        case reviewConstants.REVIEW_GET_BY_EMPLOYEE_FAILURE:
            return {};

        default:
            return state;
    }
}