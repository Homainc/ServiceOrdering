import React, { useEffect } from 'react';
import * as Yup from 'yup';
import { LoadingContainer, Rating, ValidationTextField, FormikDatePicker, LoadingButton } from '../_components';
import { useParams } from 'react-router-dom';
import { employeeActions, orderActions } from '../_actions';
import { connect } from 'react-redux';
import { Card, Col, Row, Badge } from 'reactstrap';
import { Formik, Form } from 'formik';

const MakeOrderPage = props => {
    const { employeeId } = useParams();
    const { loadEmployeeProfile, employeeProfile } = props;

    useEffect(() => {
        loadEmployeeProfile(employeeId);
    }, [ employeeId, loadEmployeeProfile ]);

    return(
        <LoadingContainer isLoading={props.employeeProfileLoading}>
            <Card className='bg-light' body>
                <Row>
                    <Col xs='4' sm='4' md='3' lg='3' xl='2'>
                        <img src={employeeProfile && employeeProfile.user.imageUrl} className="rounded" height="150" width="150" alt="employee"/>
                    </Col>
                    <Col xm='4' sm='4'>
                        <h5>{employeeProfile && employeeProfile.user.firstName} {employeeProfile && employeeProfile.user.lastName}</h5>
                        <h5><Badge color="info">{employeeProfile && employeeProfile.serviceType}</Badge></h5>
                        <p>{(employeeProfile && employeeProfile.user.phoneNumber) || 'Phone number not specified'}</p>
                        <a href={`mailto:${employeeProfile && employeeProfile.user.email}`}>{employeeProfile && employeeProfile.user.email}</a>
                    </Col>
                    <Col className="text-center">
                        Average rate<br/>
                        <Rating rate={3.4} reviews={10}/><br/>
                        <span className="text-success">Service cost: $ {employeeProfile && employeeProfile.serviceCost.toFixed(2)}</span>
                    </Col>
                </Row>
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
                            clientId: props.user.id,
                            employeeId: employeeProfile.id,
                            date: values.date,
                            briefTask: values.briefTask,
                            serviceDetails: values.serviceDetails,
                            address: values.address,
                            contactPhone: values.contactPhone,
                            price: employeeProfile.serviceCost
                        });
                    }}>
                    <Form>
                        <Row>
                            <Col className='mt-2'>
                                <ValidationTextField
                                    id='briefTask'
                                    name='briefTask'
                                    label='What you want employee to do? (briefly)'
                                    placeholder='e.g, delivery'/>
                                <ValidationTextField
                                    id='serviceDetails'
                                    name='serviceDetails'
                                    label='Service details'
                                    type='textarea' 
                                    placeholder='Specify your wishes to employee'/>
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
                                    placeholder='Specify the city, street'/>
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
                                    label="Your contact phone"/>
                            </Col>
                        </Row>
                        <Row className='justify-content-center p-3'>
                            <LoadingButton isLoading={props.isOrderCreating} type='submit' color='primary'>Make an order</LoadingButton>
                        </Row>
                    </Form>
                </Formik>
            </Card>
        </LoadingContainer>
    );
};

const mapStateToProps = state => {
    const { employeeProfile, employeeProfileLoading } = state.employee;
    const { isOrderCreating } = state.order;
    const { user } = state.authentication;
    return {
        isOrderCreating,
        employeeProfile,
        employeeProfileLoading,
        user
    };
};

const mapDispatchToProps = dispatch => ({
    loadEmployeeProfile: id => dispatch(employeeActions.loadEmployeeProfile(id)),
    createOrder: order => dispatch(orderActions.createOrder(order))
});

const connectedMakeOrderPage = connect(mapStateToProps, mapDispatchToProps) (MakeOrderPage);
export { connectedMakeOrderPage as MakeOrderPage };