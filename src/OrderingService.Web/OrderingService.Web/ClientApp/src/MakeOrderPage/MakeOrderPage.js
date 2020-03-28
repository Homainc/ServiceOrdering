import React, { useEffect } from 'react';
import { LoadingContainer, Rating, ValidationTextField } from '../components';
import { useParams } from 'react-router-dom';
import { employeeActions } from '../actions';
import { connect } from 'react-redux';
import { Card, Col, Row, Button, Badge, Form } from 'reactstrap';
import { Formik } from 'formik';

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
                        <p>{employeeProfile && employeeProfile.user.phoneNumber || 'Phone number not specified'}</p>
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
                <Formik>
                    <Form>
                        <Row>
                            <Col className='mt-2'>
                                <ValidationTextField id='briefTask' name='briefTask' label='What you want employee to do? (briefly)' placeholder='e.g, delivery'/>
                                <ValidationTextField id='serviceDetails' name='serviceDetails' label='Service details' type='textarea' placeholder='Specify your wishes to employee'/>
                            </Col>
                        </Row>
                        <hr/>
                        <h5>Address</h5>
                        <Row>
                            <Col className='mt-2'>
                                <ValidationTextField id='address' name='address' label='Service completion address' placeholder='Specify the city, street'></ValidationTextField>
                            </Col>
                        </Row>
                        <hr/>
                        <h5>Approximate date of order completion</h5>
                        <Row>
                            <Col className='mt-2'>
                                <p>DATE</p>
                            </Col>
                        </Row>
                        <Row className='justify-content-center p-3'>
                            <Button color='primary'>Make an order</Button>
                        </Row>
                    </Form>
                </Formik>
            </Card>
        </LoadingContainer>
    );
};

const mapStateToProps = state => {
    const { employeeProfile, employeeProfileLoading } = state.employee;
    return {
        employeeProfile,
        employeeProfileLoading
    };
};

const mapDispatchToProps = dispatch => ({
    loadEmployeeProfile: id => dispatch(employeeActions.loadEmployeeProfile(id))
});

const connectedMakeOrderPage = connect(mapStateToProps, mapDispatchToProps) (MakeOrderPage);
export { connectedMakeOrderPage as MakeOrderPage };