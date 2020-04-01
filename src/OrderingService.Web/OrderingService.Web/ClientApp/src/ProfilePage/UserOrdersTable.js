import React, { useEffect } from 'react';
import { Table } from 'reactstrap';
import { UserOrderRow } from './UserOrderRow';
import { orderActions } from '../_actions';
import { connect } from 'react-redux';
import './UserOrdersTable.css';
import { ReviewModal } from './ReviewModal';

const UserOrdersTable = props => {
    const { userId, loadOrdersByUser } = props;

    useEffect(() => {
        if(!!userId)
            loadOrdersByUser(userId);
    }, [userId, loadOrdersByUser]);

    const ordersRows = props.orders && props.orders.map(order => 
        <UserOrderRow key={order.id} {...order}/>
    );
    return (
        <>
        <Table className="mt-3 table-bordered">
            <thead>
                <tr>
                    <th>Approximately date</th>
                    <th>Description</th>
                    <th>Status</th>
                    <th>Price,$</th>
                </tr>
            </thead>
            <tbody>
                {ordersRows && ordersRows.length === 0 ? (
                    <tr><td colSpan="4">You haven't made orders yet.</td></tr>
                ) : ordersRows}
            </tbody>
        </Table>
        <ReviewModal/> 
        </>
    );
};

const mapStateToProps = state => {
    const { orders, isOrdersLoading } = state.order;
    return {
        orders,
        isOrdersLoading
    };
};

const mapDispatchToProps = dispatch => ({
    loadOrdersByUser: id => dispatch(orderActions.loadOrdersByUser(id))
});

const connectedOrdersTableRow = connect(mapStateToProps, mapDispatchToProps)(UserOrdersTable);
export { connectedOrdersTableRow as UserOrdersTable };