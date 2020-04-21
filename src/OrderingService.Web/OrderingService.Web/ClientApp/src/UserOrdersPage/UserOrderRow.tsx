import React from 'react';
import { Button, UncontrolledCollapse } from 'reactstrap';
import { connect, ConnectedProps } from 'react-redux';
import './UserOrdersRow.css';
import { RootState } from '../_store';
import * as reviewModalActions from '../_store/reviewModal/actions';
import { ORDER_STATUS } from '../_store/order/types';
import { OrderDTO, OrderStatus } from '../WebApiModels';

const mapState = (state: RootState) => ({});

const mapDispatch = {
    showReviewModal: (order: OrderDTO) => reviewModalActions.show(order)
};

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type UserOrderRowProps = PropsFromRedux & Readonly<{
    status: OrderStatus;
    date: string;
    briefTask: string | undefined;
    serviceDetails: string | undefined;
    address: string | undefined;
    id: number;
    employeeId: string;
    price: number;
    clientId: string;
}>;

const UserOrderRow = (props: UserOrderRowProps) => {
    const statusText = ORDER_STATUS[props.status];
    const orderTime = new Date(props.date).toLocaleTimeString();
    const orderDate = new Date(props.date).toLocaleDateString();
    return (
        <tr>
            <td>{orderTime} {orderDate}</td>
            <td className="w-60">
                <p>{props.briefTask}</p>
                <Button color="link" className="btn-sm" id={`toggler${props.id}`}>See details</Button>
                <UncontrolledCollapse toggler={`#toggler${props.id}`}>
                    <p><span className="font-weight-bold">Service details: </span>{props.serviceDetails}</p>
                    <p><span className="font-weight-bold">Specified address: </span>{props.address}</p>
                </UncontrolledCollapse>
            </td>
            <td>
                {statusText}
                {props.status === 1 && (
                <Button color="link"
                    onClick={() => props.showReviewModal({
                        status: props.status,
                        briefTask: props.briefTask,
                        address: props.address,
                        serviceDetails: props.serviceDetails,
                        date: props.date,
                        price: props.price,
                        employeeId: props.employeeId,
                        clientId: props.clientId,
                        id: props.id
                    })}>Confirm completion</Button>
                )}</td>
            <td>{props.price.toFixed(2)}</td>
        </tr>
    );
};

const connectedUserOrderRow = connector(UserOrderRow);
export { connectedUserOrderRow as UserOrderRow };