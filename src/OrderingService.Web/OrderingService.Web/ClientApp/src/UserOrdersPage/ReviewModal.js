import React from 'react';
import * as Yup from 'yup';
import { reviewModalActions, reviewActions, orderActions } from '../_actions';
import { connect } from 'react-redux';
import { Modal, ModalBody, ModalHeader, ModalFooter, Button } from 'reactstrap';
import { Formik, Form } from 'formik';
import { ValidationTextField, RatingInput, LoadingButton } from '../_components';

const ReviewModal = props => {

    const createReview = async values => {
        await props.createReview({
            text: values.text,
            rate: values.rate,
            employeeId: props.order.employeeId
        });
        await props.confirmOrder(props.order.id);
        props.closeModal();
    };

    const confirmOrder = async () => {
        await props.confirmOrder(props.order.id);
        props.closeModal();
    };

    return(
        <Modal isOpen={props.isModalOpened} toggle={props.closeModal}>
            <Formik
            initialValues={{
                text: '',
                rate: 0
            }}
            validationSchema={Yup.object({
                text: Yup.string()
                    .required("Text is required")
                    .max(255)
            })}
            onSubmit={createReview}>
                <Form>
                    <ModalHeader toggle={props.closeModal}>Completion of order</ModalHeader>
                    <ModalBody>
                        <p>Please leave a review about the provided service.</p>
                            <RatingInput 
                                id="rate" 
                                name="rate"
                                label="Rate the service"/>
                            <ValidationTextField
                                label="Text" 
                                id="text" 
                                name="text"
                                type="textarea"
                                disabled={props.isOrderConfirming || props.isReviewCreating}/>
                    </ModalBody>
                    <ModalFooter>
                        <LoadingButton isLoading={props.isReviewCreating || props.isOrderConfirming} color="success" type="submit">Leave a review</LoadingButton>
                        <LoadingButton isLoading={props.isOrderConfirming} color="primary" onClick={confirmOrder}>Confirm completion</LoadingButton>
                        <Button color="secondary" onClick={props.closeModal} disabled={props.isOrderConfirming || props.isReviewCreating}>Cancel</Button>
                    </ModalFooter>
                </Form>
            </Formik>
        </Modal>
    );
};

const mapStateToProps = state => {
    const { isModalOpened, order } = state.reviewModal;
    const { isOrderConfirming } = state.order;
    const { isReviewCreating } = state.review;
    return {
        isModalOpened,
        isOrderConfirming,
        isReviewCreating,
        order
    };
};

const mapDispatchToProps = dispatch => ({
    closeModal: () => dispatch(reviewModalActions.closeModal()),
    createReview: review => dispatch(reviewActions.createReview(review)),
    confirmOrder: id => dispatch(orderActions.confirmOrder(id))
});

const connectedReviewModal = connect(mapStateToProps, mapDispatchToProps)(ReviewModal);
export { connectedReviewModal as ReviewModal };