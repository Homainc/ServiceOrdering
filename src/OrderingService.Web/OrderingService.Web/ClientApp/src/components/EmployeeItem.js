import React from 'react';
import { ListGroup, ListGroupItem, Row, Col, Button } from 'reactstrap';
import { Link } from 'react-router-dom';

export const EmployeeItem = props => {
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
                    <img src={props.employee.user.imageUrl} className="rounded" height="130" width="130" alt="employee"/>
                    </Col>
                    <Col><p className="text-bold">Description:</p>
                <p className="text-secondary">{props.employee.description}</p></Col>
                </Row>
            </ListGroupItem>
            <ListGroupItem>
                <Row>
                    <Col>{props.employee.user.firstName} {props.employee.user.lastName}</Col>
                    <Col lg="2" md="3" sm="4" xs="6" className="align-self-end text-right">
                        <Button color="link" size="sm" className="mr-2">Details</Button>
                        <Button size="sm" color="success" outline tag={Link} to={`/employee/${props.employee.id}`}>Employ</Button>
                    </Col>
                </Row>
                </ListGroupItem>
        </ListGroup>
    );
};