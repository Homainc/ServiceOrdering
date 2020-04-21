import React from 'react';
import { Button } from 'reactstrap';
import { connect, ConnectedProps } from 'react-redux';
import { RootState } from '../_store';
import { OrderActionTypes, OrderState, ORDER_STATUS } from '../_store/order/types';
import { ThunkDispatch } from 'redux-thunk';
import * as orderActions from '../_store/order/actions';

const mapState = (state: RootState) => ({
    orderAccepting: state.order.accepting,
    orderDeclining: state.order.declining
});

const mapDispatch = (
    dispatch: ThunkDispatch<OrderState, undefined, OrderActionTypes>
) => ({
    acceptOrder: (id: number) => dispatch(orderActions.accept(id)),
    declineOrder: (id: number) => dispatch(orderActions.decline(id))
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type EmployeeOrderStatusProps = PropsFromRedux & Readonly<{
    status: number;
    orderId: number;
}>;

const EmployeeOrderStatus = (props: EmployeeOrderStatusProps) => {
    const statusText = ORDER_STATUS[props.status];

    return (
        <>
        <span className="font-italic">{statusText}</span>
        {props.status === 0 && (
        <>
            <Button color='link'
                className='text-success btn-sm'
                onClick={() => props.acceptOrder(props.orderId)}
                disabled={props.orderAccepting || props.orderDeclining}>Accept</Button>
            <Button color='link' 
                className='text-danger btn-sm' 
                onClick={() => props.declineOrder(props.orderId)}
                disabled={props.orderAccepting || props.orderDeclining}>Decline</Button>
        </>)}
        </>
    );
};

const connectectOrderStatus = connector(EmployeeOrderStatus);
export { connectectOrderStatus as EmployeeOrderStatus };