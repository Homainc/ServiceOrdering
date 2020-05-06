import React, { useEffect, useState } from 'react';
import { Card, CardText, CardTitle, Row, Col, Button } from 'reactstrap';
import { UserWithAvatar, Rating } from '../_components';
import { connect, ConnectedProps } from 'react-redux';
import { RootState } from '../_store';
import { ThunkDispatch } from 'redux-thunk';
import * as reviewActions from '../_store/review/actions'; 
import { ReviewActionTypes, ReviewState } from '../_store/review/types';

const mapState = (state: RootState) => ({
    reviews: state.review.reviews,
    totalReviews: state.review.totalReviews
});

const mapDispatch = (
    dispatch: ThunkDispatch<ReviewState, undefined, ReviewActionTypes>
) => ({
    loadReviewsByEmployee: (employeeId: string, page: number) => dispatch(reviewActions.loadListByEmployee(employeeId, page))
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type ReviewBlockProps = PropsFromRedux & Readonly<{
    employeeId?: string;
}>;

const ReviewsBlock = (props: ReviewBlockProps) => {
    const [reviewsPage, setReviewsPage] = useState(1);

    const { employeeId, loadReviewsByEmployee } = props;

    useEffect(() => {
        if(employeeId)
            loadReviewsByEmployee(employeeId, reviewsPage);
    }, [employeeId, loadReviewsByEmployee, reviewsPage]);

    const reviews = props.reviews?.map(review => 
        <Card body key={review.id} className="my-2">
            <CardTitle>
                <UserWithAvatar user={review.client}/>
                <Rating className="mt-1" rate={review.rate}/>
            </CardTitle>
            <CardText>
                {review.text}
            </CardText>    
            <CardText>
                <span className="text-secondary font-italic small">
                    {new Date(review.date).toLocaleTimeString()}, {new Date(review.date).toLocaleDateString()}
                </span>
            </CardText>
        </Card>   
    );

    return(
        <>
        <hr/>
        <Row>
            <Col><h5>Reviews ({props.totalReviews})</h5></Col>
        </Row>
        {reviews?.length === 0 ? (
            <p className="text-secondary">The employee haven't got reviews yet.</p>
        ): (<>
            {reviews}
            {reviews?.length !== props.totalReviews && (
                <Button 
                    className='d-flex align-self-center' 
                    outline 
                    onClick={() => setReviewsPage(reviewsPage + 1)}>
                        Show more
                </Button>
            )}
        </>)}
        </>
    );
};

const connectedReviewsBlock = connector(ReviewsBlock);
export { connectedReviewsBlock as ReviewsBlock };