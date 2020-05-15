import { OrderDto } from "../../WebApiModels";
import { 
    ReviewModalActionTypes, 
    REVIEW_MODAL_SHOW, REVIEW_MODAL_HIDE 
} from "./types";

export function show(
    order: OrderDto
): ReviewModalActionTypes {
    return { type: REVIEW_MODAL_SHOW, order };
}

export function close(): ReviewModalActionTypes {
    return { type: REVIEW_MODAL_HIDE };
};