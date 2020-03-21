import React from 'react';
import { Col, Row, ListGroupItem, ListGroupItemHeading, ListGroupItemText, Fade } from 'reactstrap';

export const UserProfileBlock = props => {
    return(
        <ListGroupItem>
            <Row>
                <Col md='3'>
                    <img className="rounded-circle border border-dark" src={props.profile && props.profile.imageUrl} height="130" width="130"></img>
                </Col>
                <Col>
                    <ListGroupItemHeading>First name</ListGroupItemHeading>
                    <ListGroupItemText>{props.profile && props.profile.firstName}</ListGroupItemText>
                    <ListGroupItemHeading>Last name</ListGroupItemHeading>
                    <ListGroupItemText>{props.profile && props.profile.lastName}</ListGroupItemText>
                </Col>
            </Row>
        </ListGroupItem>
    );
};