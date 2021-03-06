import { Row, Col, Badge, Button } from 'reactstrap';
import { Link } from 'react-router-dom';
import { Rating } from './Rating';
import { EmployeeProfileDto } from '../WebApiModels';
import React from 'react';
import { cloudinary } from '../_helpers';

type EmployeeOverviewProps = {
    employee?: EmployeeProfileDto;
    withHireButton: boolean;
};

export const EmployeeOverview = (props: EmployeeOverviewProps) => {
    const { employee } = props;
    return (
        <Row>
            <Col xs='4' sm='4' md='3' lg='3' xl='2'>
                <img src={cloudinary.url(employee?.user?.imagePublicId || 'default-employee')} className="rounded" height="150" width="150" alt="employee"/>
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
                {props.withHireButton ? (
                    <Button tag={Link} 
                        to={`/order/${employee?.id}`} 
                        className="my-2" 
                        color="success" 
                        outline>
                            Hire for $ {employee?.serviceCost.toFixed(2)}
                    </Button>
                ): (
                    <span className="text-success">Service cost: $ {employee?.serviceCost.toFixed(2)}</span>
                )}
            </Col>
        </Row>
    );
};