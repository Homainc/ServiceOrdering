import { reviewConstants } from "../_constants";
import { reviewService } from "../_services";

export const reviewActions = {
    createReview,
    getReviewsByEmployee,
};

function createReview(review){
    return dispatch => {
        dispatch(request());

        return reviewService.createReview(review)
            .then(review => {
                dispatch(success());
            }, 
            error => {
                dispatch(failure());
            });
    };

    function request() { return { type: reviewConstants.REVIEW_CREATE_REQUEST }; }
    function success() { return { type: reviewConstants.REVIEW_CREATE_SUCCESS }; }
    function failure() { return { type: reviewConstants.REVIEW_CREATE_FAILURE }; }
}

function getReviewsByEmployee(employeeId){
    return dispatch => {
        dispatch(request());

        return reviewService.getReviewsByEmployee(employeeId)
            .then(data => {
                dispatch(success(data.value));
            }, error => {
                dispatch(failure());
            });
    };

    function request() { return { type: reviewConstants.REVIEW_GET_BY_EMPLOYEE_REQUEST }; }
    function success(reviews) { return { type: reviewConstants.REVIEW_GET_BY_EMPLOYEE_SUCCESS, reviews }; }
    function failure() { return { type: reviewConstants.REVIEW_GET_BY_EMPLOYEE_FAILURE }; }
}