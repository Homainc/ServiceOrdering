import React from 'react';
import { Button, UncontrolledCollapse } from 'reactstrap';
import { OrderStatus } from './OrderStatus'; 

export const EmployeeOrderRow = props => {
    return (
        <tr>
            <td>{new Date(props.date).toLocaleDateString()} {new Date(props.date).toLocaleTimeString()}</td>
            <td className="w-60">
                <p>{props.briefTask}</p>
                <Button color="link" className="btn-sm" id={`toggler${props.id}`}>See details</Button>
                <UncontrolledCollapse toggler={`#toggler${props.id}`}>
                    <p><span className="font-weight-bold">Service details: </span>{props.serviceDetails}</p>
                    <p><span className="font-weight-bold">Client address: </span>{props.address}</p>
                    <p><span className="font-weight-bold">Client contact phone: </span>{props.contactPhone}</p>
                </UncontrolledCollapse>
            </td>
            <td><OrderStatus orderId={props.id} status={props.status}/></td>
            <td>{props.price.toFixed(2)}</td>
        </tr>
    );
};