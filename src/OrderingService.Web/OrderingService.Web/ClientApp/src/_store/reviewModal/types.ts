import { OrderDto } from "../../WebApiModels";

export const REVIEW_MODAL_SHOW = 'reviewModal/show';
export const REVIEW_MODAL_HIDE = 'reviewModal/hide';

interface ReviewModalShowAction {
    type: typeof REVIEW_MODAL_SHOW;
    order: OrderDto;
};

interface ReviewModalHideAction {
    type: typeof REVIEW_MODAL_HIDE;
};

export type ReviewModalActionTypes = 
    ReviewModalHideAction | 
    ReviewModalShowAction;

export interface ReviewModalState {
    modalOpened: boolean;
    order: OrderDto | undefined;
};