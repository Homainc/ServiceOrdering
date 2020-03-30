import React, { useEffect } from 'react';
import { Table } from 'reactstrap';
import { OrdersTableRow } from './OrdersTableRow';
import { orderActions } from '../actions';
import { connect } from 'react-redux';
import './OrdersTable.css';

const OrdersTable = props => {
    const { userId, loadOrdersByUser } = props;

    useEffect(() => {
        loadOrdersByUser(userId);
    }, [userId, loadOrdersByUser]);

    const ordersRows = props.orders && props.orders.map(order => 
        <OrdersTableRow key={order.id} {...order}/>
    );
    return (
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