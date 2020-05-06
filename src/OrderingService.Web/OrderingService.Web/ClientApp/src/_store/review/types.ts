import { ReviewDTO } from "../../WebApiModels";

export const REVIEW_CREATE_REQUEST = 'review/create (request)';
export const REVIEW_CREATE_SUCCESS = 'review/create (success)';
export const REVIEW_CREATE_FAILURE = 'review/create (failure)';

export const REVIEW_LOAD_LIST_BY_EMPLOYEE_REQUEST = 'review/load_list_by_employee (request)';
export const REVIEW_LOAD_LIST_BY_EMPLOYEE_SUCCESS = 'review/load_list_by_employee (success)';
export const REVIEW_LOAD_LIST_BY_EMPLOYEE_FAILURE = 'review/load_list_by_employee (failure)';

export const REVIEW_SHOW_MODAL = 'review/show_modal';

interface ReviewRequestAction {
    type: typeof REVIEW_CREATE_REQUEST | typeof REVIEW_LOAD_LIST_BY_EMPLOYEE_REQUEST;
};

interface ReviewCreateSuccessAction {
    type: typeof REVIEW_CREATE_SUCCESS;
    review: ReviewDTO;
};

interface ReviewLoadListAction {
    type: typeof REVIEW_LOAD_LIST_BY_EMPLOYEE_SUCCESS;
    list: Array<ReviewDTO>;
    total: number;
    page: number;
};

interface ReviewFailureAction {
    type: typeof REVIEW_CREATE_FAILURE | typeof REVIEW_LOAD_LIST_BY_EMPLOYEE_FAILURE;
    error: string;
};

export type ReviewActionTypes = 
    ReviewRequestAction |
    ReviewCreateSuccessAction |
    ReviewLoadListAction |
    ReviewFailureAction;

export interface ReviewState {
    creating: boolean;
    reviews?: Array<ReviewDTO>;
    totalReviews: number;
};
    