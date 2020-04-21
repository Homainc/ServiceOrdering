import React from 'react';
import * as Yup from 'yup';
import { connect, ConnectedProps } from 'react-redux';
import { Modal, ModalBody, ModalHeader, ModalFooter, Button } from 'reactstrap';
import { Formik, Form } from 'formik';
import { ValidationTextField, RatingInput, LoadingButton } from '../_components';
import { RootState } from '../_store';
import { ReviewActionTypes } from '../_store/review/types';
import { ReviewModalActionTypes } from '../_store/reviewModal/types';
import { OrderActionTypes } from '../_store/order/types';
import { ThunkDispatch } from 'redux-thunk';
import * as reviewModalActions from '../_store/reviewModal/actions';
import * as reviewActions from '../_store/review/actions';
import * as orderActions from '../_store/order/actions';
import { ReviewDTO } from '../WebApiModels';

const mapState = (state: RootState) => ({
    modalOpened: state.reviewModal.modalOpened,
    orderConfirming: state.order.confirming,
    reviewCreating: state.review.creating,
    order: state.reviewModal.order
});

const mapDispatch = (
    dispatch: ThunkDispatch<RootState, undefined, ReviewActionTypes | ReviewModalActionTypes | OrderActionTypes>
) => ({
    closeModal: () => dispatch(reviewModalActions.close()),
    createReview: async (review: ReviewDTO) => dispatch(reviewActions.create(review)),
    confirmOrder: async (id: number) => dispatch(orderActions.confirm(id))
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type ReviewModalProps = PropsFromRedux & Readonly<{}>;

const ReviewModal = (props: ReviewModalProps) => {

    const createReview = async (values: {
        text: string;
        rate: number;
    }) => {
        if(props.order){
            await props.createReview({
                date: new Date(Date.now()).toDateString(),
                text: values.text,
                rate: values.rate,
                clientId: props.order.clientId,
                employeeId: props.order.employeeId
            });
            await props.confirmOrder(props.order.id as number);
        }
        props.closeModal();
    };

    const confirmOrder = async () => {
        if(props.order)
            await props.confirmOrder(props.order.id as number);
        props.closeModal();
    };

    return(
        <Modal isOpen={props.modalOpened} toggle={props.closeModal}>
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
                                disabled={props.orderConfirming || props.reviewCreating}/>
                    </ModalBody>
                    <ModalFooter>
                        <LoadingButton isLoading={props.reviewCreating || props.orderConfirming} color="success" type="submit">Leave a review</LoadingButton>
                        <LoadingButton isLoading={props.orderConfirming} color="primary" onClick={confirmOrder}>Confirm completion</LoadingButton>
                        <Button color="secondary" onClick={props.closeModal} disabled={props.orderConfirming || props.reviewCreating}>Cancel</Button>
                    </ModalFooter>
                </Form>
            </Formik>
        </Modal>
    );
};

const connectedReviewModal = connector(ReviewModal);
export { connectedReviewModal as ReviewModal };