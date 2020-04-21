import { OrderDTO } from "../../WebApiModels";
import { 
    ReviewModalActionTypes, 
    REVIEW_MODAL_SHOW, REVIEW_MODAL_HIDE 
} from "./types";

export function show(
    order: OrderDTO
): ReviewModalActionTypes {
    return { type: REVIEW_MODAL_SHOW, order };
}

export function close(): ReviewModalActionTypes {
    return { type: REVIEW_MODAL_HIDE };
};