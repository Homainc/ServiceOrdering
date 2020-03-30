import React from 'react';
import { Button, UncontrolledCollapse } from 'reactstrap';
import { orderConstants } from '../constants';

export const OrdersTableRow = props => {
    return (
        <tr>
            <td>{new Date(props.date).toLocaleDateString()} {new Date(props.date).toLocaleTimeString()}</td>
            <td className="w-60">
                <p>{props.briefTask}</p>
                <Button color="link" className="btn-sm" id={`toggler${props.id}`}>See details</Button>
                <UncontrolledCollapse toggler={`#toggler${props.id}`}>
                    <p><span className="font-weight-bold">Service details: </span>{props.serviceDetails}</p>
                    <p><span className="font-weight-bold">Specified address: </span>{props.address}</p>
                </UncontrolledCollapse>
            </td>
            <td>{orderConstants.STATUS[props.status]}</td>
            <td>{props.price.toFixed(2)}</td>
        </tr>
    );
};