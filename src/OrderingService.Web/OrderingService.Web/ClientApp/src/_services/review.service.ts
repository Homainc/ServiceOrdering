import { api } from "../_helpers";
import { ReviewDTO } from "../WebApiModels";

export const reviewService = {
    createReview,
    getReviewsByEmployee
};

async function createReview(review: ReviewDTO): Promise<ReviewDTO> {
    const resp = await api.Review_Create({ reviewDto: review });
    return resp.body as ReviewDTO;
}

async function getReviewsByEmployee(employeeId: string): Promise<any> {
    const resp = await api.Review_GetUserReviews({ id: employeeId });
    return resp.body;
}