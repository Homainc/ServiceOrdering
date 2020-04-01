import { reviewModalConstants } from "../_constants";

export const reviewModalActions = {
    showModal,
    closeModal
};

function showModal(order){
    return { type: reviewModalConstants.REVIEW_MODAL_SHOW, order };
}

function closeModal(){
    return { type: reviewModalConstants.REVIEW_MODAL_CLOSE };
};