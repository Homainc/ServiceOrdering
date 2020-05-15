import React, { useEffect } from 'react';
import * as Yup from 'yup';
import { LoadingContainer, ValidationTextField, FormikDatePicker, LoadingButton, EmployeeOverview } from '../_components';
import { useParams } from 'react-router-dom';
import { connect, ConnectedProps } from 'react-redux';
import { Card, Col, Row } from 'reactstrap';
import { Formik, Form } from 'formik';
import { RootState } from '../_store';
import { OrderActionTypes } from '../_store/order/types';
import { ThunkDispatch } from 'redux-thunk';
import * as orderActions from '../_store/order/actions';
import * as employeeActions from '../_store/employee/actions';
import { OrderCreateDto } from '../WebApiModels';
import { EmployeeActionTypes } from '../_store/employee/types';

const mapState = (state: RootState) => ({
    orderCreating: state.order.creating,
    employeeProfile: state.employee.employee,
    employeeLoading: state.employee.loading,
    user: state.auth.user
});

const mapDispatch = (
    dispatch: ThunkDispatch<RootState, undefined, OrderActionTypes | EmployeeActionTypes>
) => ({
    createOrder: (order: OrderCreateDto) => dispatch(orderActions.create(order)),
    loadEmployeeProfile: (id: string) => dispatch(employeeActions.load(id))
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type MakeOrderPageProps = PropsFromRedux & Readonly<{}>;

const MakeOrderPage = (props: MakeOrderPageProps) => {
    const { employeeId } = useParams();
    const { loadEmployeeProfile, employeeProfile } = props;

    useEffect(() => {
        loadEmployeeProfile(employeeId as string);
    }, [ employeeId, loadEmployeeProfile ]);

    return(
        <LoadingContainer isLoading={props.employeeLoading}>
            <Card className='bg-light' body>
                <EmployeeOverview withHireButton={false} employee={employeeProfile}/>
                <hr/>
                <h5>Order details</h5>
                <Formik
                    initialValues={{
                        date: new Date(Date.now()),
                        briefTask: '',
                        serviceDetails: '',
                        address: '',
                        contactPhone: ''
                    }}
                    validationSchema={Yup.object({
                        date: Yup.date(),
                        briefTask: Yup.string()
                            .required('Brief task is required')
                            .max(50, 'Brief task must be at most 50 characters'),
                        serviceDetails: Yup.string()
                            .required('Service details is required')
                            .max(255, 'Service details must be at most 255 characters'),
                        address: Yup.string()
                            .required('Address is required')
                            .max(50, 'Address must be at most 50 characters'),
                        contactPhone: Yup.string()
                            .required('Your contact phone is required')
                            .max(20, 'Your contact phone must be at most 20 characters')
                    })}
                    onSubmit={(values) => {
                        props.createOrder({
                            clientId: props.user?.id as string,
                            employeeId: employeeProfile?.id as string,
                            date: values.date.toDateString(),
                            briefTask: values.briefTask,
                            serviceDetails: values.serviceDetails,
                            address: values.address,
                            contactPhone: values.contactPhone,
                            price: employeeProfile?.serviceCost as number
                        });
                    }}>
                    <Form>
                        <Row>
                            <Col className='mt-2'>
                                <ValidationTextField
                                    id='briefTask'
                                    name='briefTask'
                                    label='What you want employee to do? (briefly)'
                                    placeholder='e.g, delivery'
                                    disabled={props.orderCreating}/>
                                <ValidationTextField
                                    id='serviceDetails'
                                    name='serviceDetails'
                                    label='Service details'
                                    type='textarea' 
                                    placeholder='Specify your wishes to employee'
                                    disabled={props.orderCreating}/>
                            </Col>
                        </Row>
                        <hr/>
                        <h5>Address</h5>
                        <Row>
                            <Col className='mt-2'>
                                <ValidationTextField 
                                    id='address' 
                                    name='address' 
                                    label='Service completion address' 
                                    placeholder='Specify the city, street'
                                    disabled={props.orderCreating}/>
                            </Col>
                        </Row>
                        <hr/>
                        <h5>Approximate date of order completion</h5>
                        <Row>
                            <Col className='mt-2'>
                                <FormikDatePicker 
                                    id="date" 
                                    name="date" 
                                    label="Specify the date"/>
                            </Col>
                        </Row>
                        <hr/>
                        <h5>Contacts</h5>
                        <Row>
                            <Col className='mt-2'>
                                <ValidationTextField 
                                    id="contactPhone" 
                                    name="contactPhone"
                                    placeholder="Specify your mobile or home number" 
                                    label="Your contact phone"
                                    disabled={props.orderCreating}/>
                            </Col>
                        </Row>
                        <Row className='justify-content-center p-3'>
                            <LoadingButton isLoading={props.orderCreating} type='submit' color='primary'>Make an order</LoadingButton>
                        </Row>
                    </Form>
                </Formik>
            </Card>
        </LoadingContainer>
    );
};

const connectedMakeOrderPage = connector(MakeOrderPage);
export { connectedMakeOrderPage as MakeOrderPage };