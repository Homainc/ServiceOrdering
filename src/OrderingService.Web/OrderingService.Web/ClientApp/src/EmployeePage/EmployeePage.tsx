import React, { useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import { Card, Row, Col, Button, Badge } from 'reactstrap';
import { LoadingContainer, Rating } from '../_components';
import { ReviewsBlock } from './ReviewsBlock';
import { connect, ConnectedProps } from 'react-redux';
import '@fortawesome/fontawesome-free/css/all.css';
import { RootState } from '../_store';
import { EmployeeState, EmployeeActionTypes } from '../_store/employee/types';
import { ThunkDispatch } from 'redux-thunk';
import * as employeeActions from '../_store/employee/actions';

const mapState = (state: RootState) => ({
    employeeProfileLoading: state.employee.loading,
    employeeProfile: state.employee.employee
});

const mapDispatch = (
    dispatch: ThunkDispatch<EmployeeState, undefined, EmployeeActionTypes>
) => ({
    loadEmployeeProfile: (id: string) => dispatch(employeeActions.load(id))
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type EmployeePageProps = PropsFromRedux & {
    employeeId: string;
};

const EmployeePage = (props: EmployeePageProps) => {

    const { id } = useParams();
    const { loadEmployeeProfile } = props;

    useEffect(() => {
        loadEmployeeProfile(id as string);
    }, [ id, loadEmployeeProfile ]);

    const { employeeProfile } = props;

    return(
        <LoadingContainer isLoading={props.employeeProfileLoading}>
            <Card body className="bg-light">
                <Row>
                    <Col xs='4' sm='4' md='3' lg='3' xl='2'>
                        <img src={employeeProfile?.user?.imageUrl || 'images/default-user.jpg'} className="rounded" height="150" width="150" alt="employee"/>
                    </Col>
                    <Col xm='4' sm='4'>
                        <h5>{employeeProfile?.user?.firstName} {employeeProfile && employeeProfile?.user?.lastName}</h5>
                        <h5><Badge color="info">{employeeProfile?.serviceType}</Badge></h5>
                        <p>{employeeProfile?.user?.phoneNumber || 'Phone number not specified'}</p>
                        <a href={`mailto:${employeeProfile?.user?.email}`}>{employeeProfile?.user?.email}</a>
                    </Col>
                    <Col className="text-center">
                        Average rate<br/>
                        {employeeProfile && employeeProfile.reviewsCount > 0? (
                            <Rating rate={employeeProfile.averageRate} reviews={employeeProfile.reviewsCount}/>
                        ):(
                            <span className="text-secondary">No reviews</span>
                        )}<br/>
                        <Button tag={Link} to={`/order/${employeeProfile?.id}`} className="my-2" color="success" outline>Hire for $ {employeeProfile?.serviceCost.toFixed(2)}</Button>
                    </Col>
                </Row>
                <hr/>
                <Row>
                    <Col>
                    <h5>Description</h5>
                    <p className="text-secondary" >{employeeProfile && employeeProfile.description}</p>
                    </Col>
                </Row>
                <ReviewsBlock employeeId={employeeProfile?.id}/>
            </Card>
        </LoadingContainer>
    );
};

const connectedEmployeePage = connector(EmployeePage);
export { connectedEmployeePage as EmployeePage };