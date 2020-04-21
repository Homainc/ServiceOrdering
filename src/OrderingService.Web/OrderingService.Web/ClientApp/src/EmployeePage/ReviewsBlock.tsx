import React, { useEffect } from 'react';
import { Card, CardText, CardTitle, Row, Col } from 'reactstrap';
import { UserWithAvatar, Rating } from '../_components';
import { connect, ConnectedProps } from 'react-redux';
import { RootState } from '../_store';
import { ThunkDispatch } from 'redux-thunk';
import * as reviewActions from '../_store/review/actions'; 
import { ReviewActionTypes, ReviewState } from '../_store/review/types';

const mapState = (state: RootState) => ({
    reviews: state.review.reviews
});

const mapDispatch = (
    dispatch: ThunkDispatch<ReviewState, undefined, ReviewActionTypes>
) => ({
    loadReviewsByEmployee: (employeeId: string) => dispatch(reviewActions.loadListByEmployee(employeeId))
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type ReviewBlockProps = PropsFromRedux & Readonly<{
    employeeId: string | undefined;
}>;

const ReviewsBlock = (props: ReviewBlockProps) => {
    const { employeeId, loadReviewsByEmployee } = props;
    useEffect(() => {
        if(employeeId)
            loadReviewsByEmployee(employeeId);
    }, [employeeId, loadReviewsByEmployee]);

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
            <Col><h5>Reviews ({reviews?.length || 0})</h5></Col>
        </Row>
        {reviews && reviews.length === 0 ? (
            <p className="text-secondary">The employee haven't got reviews yet.</p>
        ): reviews}
        </>
    );
};

const connectedReviewsBlock = connector(ReviewsBlock);
export { connectedReviewsBlock as ReviewsBlock };