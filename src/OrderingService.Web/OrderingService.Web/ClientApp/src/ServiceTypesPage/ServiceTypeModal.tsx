import React from 'react';
import * as Yup from 'yup';
import { connect, ConnectedProps } from 'react-redux';
import { Modal, ModalBody, ModalHeader, ModalFooter, Button } from 'reactstrap';
import { Formik, Form } from 'formik';
import { ValidationTextField, LoadingButton } from '../_components';
import { RootState } from '../_store';
import { ThunkDispatch } from 'redux-thunk';
import * as serviceTypeActions from '../_store/serviceType/actions';
import { ServiceTypeActionTypes } from '../_store/serviceType/types';

const mapState = (state: RootState) => ({
    isProcessing: state.serviceType.isServiceProcessing
});

const mapDispatch = (
    dispatch: ThunkDispatch<RootState, undefined, ServiceTypeActionTypes>
) => ({
    create: async (name: string) => dispatch(serviceTypeActions.create({ name })),
    update: async (id: number, name: string) => dispatch(serviceTypeActions.update({ id, name }))
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type ServiceTypeModalProps = PropsFromRedux & Readonly<{
    id?: number;
    name?: string; 
    isOpen: boolean;
    closeModal: () => void;
}>;

const ServiceTypeModal = (props: ServiceTypeModalProps) => {

    return(
        <Modal isOpen={props.isOpen} toggle={props.closeModal}>
            <Formik
            initialValues={{
                id: props.id,
                name: props.name || ''
            }}
            validationSchema={Yup.object({
                name: Yup.string()
                    .required("Name is required")
                    .max(30)
            })}
            onSubmit={(values, { setErrors }) => {
                if(values.id){
                    props.update(values.id, values.name)
                        .then(() => props.closeModal())
                        .catch(err => setErrors(err));
                }
                else {
                    props.create(values.name)
                        .then(() => props.closeModal())
                        .catch(err => setErrors(err));
                }
            }}>
                <Form>
                    <ModalHeader toggle={props.closeModal}>{props.id?'Add':'Edit'} service type</ModalHeader>
                        <ModalBody>
                            <ValidationTextField
                                label="Name" 
                                id="name" 
                                name="name"
                                type="text"
                                disabled={props.isProcessing}/>
                    </ModalBody>
                    <ModalFooter>
                        <LoadingButton type="submit" isLoading={props.isProcessing} color="primary">Save</LoadingButton>
                        <Button color="secondary" onClick={props.closeModal} disabled={props.isProcessing}>Cancel</Button>
                    </ModalFooter>
                </Form>
            </Formik>
        </Modal>
    );
};

const connectedServiceTypeModal = connector(ServiceTypeModal);
export { connectedServiceTypeModal as ServiceTypeModal };