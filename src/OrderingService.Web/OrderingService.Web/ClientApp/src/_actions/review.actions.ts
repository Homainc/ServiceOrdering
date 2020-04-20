import { reviewConstants } from "../_constants";
import { reviewService } from "../_services";
import { ThunkAction } from "redux-thunk";
import { ReviewState, ReviewAction } from "../_reducers/review.reducers";
import { ReviewDTO } from "../WebApiModels";

type ReviewThunkResult<R> = ThunkAction<R, ReviewState, undefined, ReviewAction>;

export const reviewActions = {
    createReview,
    getReviewsByEmployee,
};

function createReview(review: ReviewDTO): ReviewThunkResult<void> {
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

    function request(): ReviewAction { 
        return { 
            type: reviewConstants.REVIEW_CREATE_REQUEST, 
            reviews: undefined 
        }; 
        }
    function success(): ReviewAction { 
        return { 
            type: reviewConstants.REVIEW_CREATE_SUCCESS,
            reviews: undefined
         }; 
    }
    function failure(): ReviewAction { 
        return { 
            type: reviewConstants.REVIEW_CREATE_FAILURE,
            reviews: undefined
         }; 
    }
}

function getReviewsByEmployee(employeeId: string): ReviewThunkResult<void> {
    return dispatch => {
        dispatch(request());

        return reviewService.getReviewsByEmployee(employeeId)
            .then(data => {
                dispatch(success(data.value));
            }, error => {
                dispatch(failure());
            });
    };

    function request(): ReviewAction { 
        return { 
            type: reviewConstants.REVIEW_GET_BY_EMPLOYEE_REQUEST ,
            reviews: undefined
        }; 
    }
    function success(reviews: Array<ReviewDTO>): ReviewAction { 
        return { 
            type: reviewConstants.REVIEW_GET_BY_EMPLOYEE_SUCCESS, 
            reviews 
        }; 
    }
    function failure(): ReviewAction { 
        return { 
            type: reviewConstants.REVIEW_GET_BY_EMPLOYEE_FAILURE,
            reviews: undefined
        }; 
    }
}