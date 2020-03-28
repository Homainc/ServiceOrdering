import React, { useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import { Card, Row, Col, Button } from 'reactstrap';
import { LoadingContainer, Rating } from '../components';
import { employeeActions } from '../actions';
import { connect } from 'react-redux';
import '@fortawesome/fontawesome-free/css/all.css';


const EmployeePage = props => {

    const { id } = useParams();

    useEffect(() => {
        props.loadEmployeeProfile(id);
    }, []);

    const { employeeProfile } = props;

    return(
        <LoadingContainer isLoading={props.employeeProfileLoading}>
            <Card body className="bg-light">
                <Row>
                    <Col xs='4' sm='4' md='3' lg='3' xl='2'>
                        <img src={employeeProfile && employeeProfile.user.imageUrl} className="rounded" height="150" width="150" alt="employee"/>
                    </Col>
                    <Col xm='4' sm='4'>
                        <h5>{employeeProfile && employeeProfile.user.firstName} {employeeProfile && employeeProfile.user.lastName}</h5>
                        <p>{employeeProfile && employeeProfile.user.phoneNumber}</p>
                        <a href={`mailto:${employeeProfile && employeeProfile.user.email}`}>{employeeProfile && employeeProfile.user.email}</a>
                    </Col>
                    <Col className="text-center">
                        <Button className="my-2" color="success" outline>Hire for $ {employeeProfile && employeeProfile.serviceCost.toFixed(2)}</Button>
                        <br/><Rating rate={3.4} reviews={10}/>
                    </Col>
                </Row>
                <hr/>
                <Row>
                    <Col>
                    <h5>Description</h5>
                    <p className="text-secondary" >{employeeProfile && employeeProfile.description}</p>
                    </Col>
                </Row>
                <hr/>
                <Row>
                    <Col><h5>Reviews (5)</h5></Col>
                </Row>
            </Card>
        </LoadingContainer>
    );
};

const mapStateToProps = state => {
    const { employeeProfileLoading, employeeProfile } = state.employee;
    return {
        employeeProfileLoading,
        employeeProfile
    };
};

const mapDispatchToProps = dispatch => ({
    loadEmployeeProfile: id => dispatch(employeeActions.loadEmployeeProfile(id))
});

const connectedEmployeePage = connect(mapStateToProps, mapDispatchToProps)(EmployeePage);
export { connectedEmployeePage as EmployeePage };