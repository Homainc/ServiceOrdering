import React from 'react';
import { ListGroup, ListGroupItem, Row, Col, Button } from 'reactstrap';
import { UserWithAvatar } from './UserWithAvatar';

export const EmployeeItem = props => {
    return(
        <ListGroup className="mb-3">
            <ListGroupItem>
                <Row>
                    <Col>Service type: {props.employee.serviceType}</Col>
                    <Col lg="2" md="2" sm="3" xs="4" className="text-primary align-self-end text-right">$ {props.employee.serviceCost.toFixed(2)}</Col>
                </Row>
            </ListGroupItem>
            <ListGroupItem>
                <p className="text-bold">Description:</p>
                <p className="text-secondary">{props.employee.description}</p>
            </ListGroupItem>
            <ListGroupItem>
                <Row>
                    <Col><UserWithAvatar user={props.employee.user}/></Col>
                    <Col lg="2" md="3" sm="4" xs="5" className="align-self-end text-right">
                        <Button color="link" size="sm">Details</Button>
                        <Button size="sm">Employ</Button>
                    </Col>
                </Row>
                </ListGroupItem>
        </ListGroup>
    );
};