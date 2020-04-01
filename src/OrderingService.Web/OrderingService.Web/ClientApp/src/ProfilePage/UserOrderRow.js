import React from 'react';
import { Button, UncontrolledCollapse } from 'reactstrap';
import { orderConstants } from '../_constants';
import { reviewModalActions, orderActions } from '../_actions';
import { connect } from 'react-redux';

const UserOrderRow = props => {
    const statusText = orderConstants.STATUS[props.status];
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
                        employeeId: props.employeeId,
                        clientId: props.clientId,
                        id: props.id
                    })}>Confirm completion</Button>
                )}</td>
            <td>{props.price.toFixed(2)}</td>
        </tr>
    );
};

const mapStateToProps = state => {
    const { isOrderConfirming } = state.order;
    return {
        isOrderConfirming
    };
};

const mapDispatchToProps = dispatch => ({
    confirmOrder: id => dispatch(orderActions.confirmOrder(id)),
    showReviewModal: order => dispatch(reviewModalActions.showModal(order))
});

const connectedUserOrderRow = connect(mapStateToProps, mapDispatchToProps)(UserOrderRow);
export { connectedUserOrderRow as UserOrderRow };