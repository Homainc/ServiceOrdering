import React, { useEffect } from 'react';
import { Table } from 'reactstrap';
import { UserOrderRow } from './UserOrderRow';
import { orderActions } from '../actions';
import { connect } from 'react-redux';
import './OrdersTable.css';
import { ReviewModal } from './ReviewModal';

const OrdersTable = props => {
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
                {ordersRows}
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

const connectedOrdersTableRow = connect(mapStateToProps, mapDispatchToProps)(OrdersTable);
export { connectedOrdersTableRow as OrdersTable };