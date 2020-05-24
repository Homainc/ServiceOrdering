import React from 'react';
import { ListGroup, ListGroupItem, Row, Col, Button, CardImg } from 'reactstrap';
import { Link } from 'react-router-dom';
import { EmployeeProfileDto } from '../WebApiModels';
import { Rating } from '../_components';
import '@fortawesome/fontawesome-free/css/all.css';
import './employee-item.scss';

type EmployeeItemProps = Readonly<{
    employee: EmployeeProfileDto;
}>;

export const EmployeeItem2 = (props: EmployeeItemProps) => {
    const { user } = props.employee;
    return(
        <ListGroup className="mb-3">
            <ListGroupItem className="bg-light">
                <Row>
                    <Col>Service type: {props.employee.serviceType}</Col>
                    <Col lg="2" md="2" sm="3" xs="4" className="text-primary align-self-end text-right font-weight-bold">$ {props.employee.serviceCost.toFixed(2)}</Col>
                </Row>
            </ListGroupItem>
            <ListGroupItem>
                <Row>
                    <Col lg="3" md="3" sm="4" xs="5">
                    <img src={user?.imageUrl || 'images/default-user.jpg'} className="rounded" height="130" width="130" alt="employee"/>
                    </Col>
                    <Col><p className="text-bold">Description:</p>
                <p className="text-secondary">{props.employee.description}</p></Col>
                </Row>
            </ListGroupItem>
            <ListGroupItem>
                <Row>
                    <Col>{user?.firstName} {user?.lastName}</Col>
                    <Col lg="2" md="3" sm="4" xs="6" className="align-self-end text-right">
                        <Button color="link" size="sm" className="mr-2" tag={Link} to={`/employee/${props.employee.id}`}>Profile</Button>
                        <Button size="sm" color="success" outline tag={Link} to={`/order/${props.employee.id}`}>Hire</Button>
                    </Col>
                </Row>
                </ListGroupItem>
        </ListGroup>
    );
};

export const EmployeeItem = (props: EmployeeItemProps) => {
    const { user } = props.employee;
    return(
        <ListGroupItem className="d-flex flex-row py-4 px-0 text-decoration-none text-dark align-items-center justify-content-center" tag={Link} to={`/employee/${props.employee.id}`}>
            <CardImg src={user?.imageUrl || 'images/default-user.jpg'} 
                    className='col-2 p-0 ml-3 rounded-circle employee-img' 
                    style={{ objectFit: 'cover', width: '100%' }}
                    alt={`${user?.firstName} avatar`}/>
            <Col lg="7" sm='6' xs='6' className='d-flex flex-column justify-content-center pl-5'>
                <h1 className='employee-h1'>{user?.firstName} {user?.lastName}</h1>
                <h2 className='employee-h2 text-secondary'>{props.employee.serviceType}</h2>
            </Col>
            <Col lg='1' sm='1' xs='1' className='d-flex flex-column justify-content-center align-items-end'>
                <Link to={`/order/${props.employee.id}`}>
                    <h3 className='employee-h3'><i className="fas fa-user-plus font-size-2"/></h3>Hire
                </Link>
            </Col>
            <Col lg='2' sm='3' xs='3' className='d-flex flex-column justify-content-center collapse-xs'>
                <h1 className='employee-h2'>$ {props.employee.serviceCost.toFixed(2)}</h1>
                <Rating rate={props.employee.averageRate}/>
            </Col>
        </ListGroupItem>
    );
};