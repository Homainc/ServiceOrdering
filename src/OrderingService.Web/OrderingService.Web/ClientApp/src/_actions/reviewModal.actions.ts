import { reviewModalConstants } from "../_constants";
import { ReviewModalAction } from "../_reducers/reviewModal.reducers";
import { OrderDTO } from "../WebApiModels";

export const reviewModalActions = {
    showModal,
    closeModal
};

function showModal(order: OrderDTO): ReviewModalAction {
    return { 
        type: reviewModalConstants.REVIEW_MODAL_SHOW, order 
    };
}

function closeModal(): ReviewModalAction {
    return { 
        type: reviewModalConstants.REVIEW_MODAL_CLOSE,
        order: undefined 
    };
};