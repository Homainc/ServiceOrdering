import React from 'react';
import { ListGroup, ListGroupItem, Row, Col, Button } from 'reactstrap';
import { Link } from 'react-router-dom';
import { EmployeeProfileDto } from '../WebApiModels';

type EmployeeItemProps = Readonly<{
    employee: EmployeeProfileDto;
}>;

export const EmployeeItem = (props: EmployeeItemProps) => {
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