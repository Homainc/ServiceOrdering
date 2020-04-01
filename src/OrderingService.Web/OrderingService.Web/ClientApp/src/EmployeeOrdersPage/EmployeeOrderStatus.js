import React from 'react';
import { orderConstants } from '../_constants';
import { Button } from 'reactstrap';
import { orderActions } from '../_actions';
import { connect } from 'react-redux';

const EmployeeOrderStatus = props => {
    const statusText = orderConstants.STATUS[props.status];

    return (
        <>
        <span className="font-italic">{statusText}</span>
        {props.status === 0 && (
        <>
            <Button color='link'
                className='text-success btn-sm'
                onClick={() => props.acceptOrder(props.orderId)}
                disabled={props.isOrderAccepting || props.isOrderDeclining}>Accept</Button>
            <Button color='link' 
                className='text-danger btn-sm' 
                onClick={() => props.declineOrder(props.orderId)}
                disabled={props.isOrderAccepting || props.isOrderDeclining}>Decline</Button>
        </>)}
        </>
    );
};

const mapStateToProps = state => {
    const { isOrderAccepting, isOrderDeclining } = state.order;
    return {
        isOrderAccepting,
        isOrderDeclining
    };
};

const mapDispatchToProps = dispatch => ({
    acceptOrder: id => dispatch(orderActions.acceptOrder(id)),
    declineOrder: id => dispatch(orderActions.declineOrder(id))
});

const connectectOrderStatus = connect(mapStateToProps, mapDispatchToProps)(EmployeeOrderStatus);
export { connectectOrderStatus as EmployeeOrderStatus };