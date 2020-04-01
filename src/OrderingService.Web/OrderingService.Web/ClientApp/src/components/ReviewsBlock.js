import React, { useEffect } from 'react';
import { Card, CardText, CardTitle, Row, Col } from 'reactstrap';
import { UserWithAvatar } from './UserWithAvatar';
import { Rating } from './Rating';
import { reviewActions } from '../actions';
import { connect } from 'react-redux';

const ReviewsBlock = props => {
    const { employeeId, loadReviewsByEmployee } = props;
    useEffect(() => {
        if(!!employeeId)
            loadReviewsByEmployee(employeeId);
    }, [employeeId, loadReviewsByEmployee]);

    const reviews = props.reviews && props.reviews.map(review => 
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
            <Col><h5>Reviews ({(reviews && reviews.length) || 0})</h5></Col>
        </Row>
        {reviews && reviews.length === 0 ? (
            <p className="text-secondary">The employee haven't got reviews yet.</p>
        ): reviews}
        </>
    );
};

const mapStateToProps = state => {
    const { reviews, isReviewsLoading } = state.review;
    return {
        reviews,
        isReviewsLoading
    };
};

const mapDispatchToProps = dispatch => ({
    loadReviewsByEmployee: employeeId => dispatch(reviewActions.getReviewsByEmployee(employeeId))
});

const connectedReviewsBlock = connect(mapStateToProps, mapDispatchToProps)(ReviewsBlock);
export { connectedReviewsBlock as ReviewsBlock };