import { api, getErrorMessageFromEx } from '../../_helpers'; 
import { ThunkAction } from "redux-thunk";
import { ReviewDTO } from "../../WebApiModels";
import { 
    ReviewState, ReviewActionTypes, 
    REVIEW_CREATE_REQUEST, REVIEW_CREATE_SUCCESS, REVIEW_CREATE_FAILURE, 
    REVIEW_LOAD_LIST_BY_EMPLOYEE_REQUEST, REVIEW_LOAD_LIST_BY_EMPLOYEE_SUCCESS, REVIEW_LOAD_LIST_BY_EMPLOYEE_FAILURE 
} from "./types";
import { PagedResult } from '../types';
import { RootState } from '..';

export function create(
    review: ReviewDTO
): ThunkAction<void, RootState, undefined, ReviewActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            review = (await api.Review_Create({ reviewDto: review })).body as ReviewDTO;

            dispatch(success(review));
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
        }
    };

    function request(): ReviewActionTypes { 
        return { type: REVIEW_CREATE_REQUEST }; 
    }
    function success(review: ReviewDTO): ReviewActionTypes { 
        return { type: REVIEW_CREATE_SUCCESS, review }; 
    }
    function failure(error: string): ReviewActionTypes { 
        return { type: REVIEW_CREATE_FAILURE, error }; 
    }
}

export function loadListByEmployee(
    employeeId: string,
    pageNumber: number
): ThunkAction<void, ReviewState, undefined, ReviewActionTypes> {
    return async dispatch => {
        dispatch(request());
        try {
            const pagedResult = (await api.Review_GetUserReviews({ id: employeeId, pageNumber })).body as PagedResult<ReviewDTO>;

            dispatch(success(pagedResult));
        }
        catch (err) {
            const errorMsg = getErrorMessageFromEx(err);
            dispatch(failure(errorMsg));
        }
    };

    function request(): ReviewActionTypes { 
        return { type: REVIEW_LOAD_LIST_BY_EMPLOYEE_REQUEST }; 
    }
    function success(pagedResult: PagedResult<ReviewDTO>): ReviewActionTypes { 
        return { type: REVIEW_LOAD_LIST_BY_EMPLOYEE_SUCCESS, list: pagedResult.value, total: pagedResult.total, page: pagedResult.pageNumber }; 
    }
    function failure(error: string): ReviewActionTypes { 
        return { type: REVIEW_LOAD_LIST_BY_EMPLOYEE_FAILURE, error }; 
    }
}