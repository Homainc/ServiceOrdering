import { Row, Col, Badge, Button } from 'reactstrap';
import { Link } from 'react-router-dom';
import { Rating } from './Rating';
import { EmployeeProfileDTO } from '../WebApiModels';
import React from 'react';

type EmployeeOverviewProps = {
    employee: EmployeeProfileDTO | undefined;
    withHireButton: boolean;
};

export const EmployeeOverview = (props: EmployeeOverviewProps) => {
    const { employee } = props;
    return (
        <Row>
            <Col xs='4' sm='4' md='3' lg='3' xl='2'>
                <img src={employee?.user?.imageUrl || 'images/default-user.jpg'} className="rounded" height="150" width="150" alt="employee"/>
            </Col>
            <Col xm='4' sm='4'>
                <h5>{employee?.user?.firstName} {employee?.user?.lastName}</h5>
                <h5><Badge color="info">{employee?.serviceType}</Badge></h5>
                <p>{employee?.user?.phoneNumber || 'Phone number not specified'}</p>
                <a href={`mailto:${employee?.user?.email}`}>{employee?.user?.email}</a>
            </Col>
            <Col className="text-center">
                Average rate<br/>
                {employee && employee.reviewsCount > 0 ? (
                <Rating rate={employee.averageRate || 0}/>
                ):(
                    <span className="text-secondary">No reviews</span>
                )}<br/>
                <span className="text-success">Service cost: $ {employee?.serviceCost.toFixed(2)}</span>
                {props.withHireButton && (
                <Button tag={Link} 
                    to={`/order/${employee?.id}`} 
                    className="my-2" 
                    color="success" 
                    outline>
                        Hire for $ {employee?.serviceCost.toFixed(2)}
                </Button>
                )}
            </Col>
        </Row>
    );
};