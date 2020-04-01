import React, { useEffect } from 'react';
import { Card, Table } from 'reactstrap';
import { LoadingContainer } from '../_components';
import { EmployeeOrderRow } from './EmployeeOrderRow';
import { connect } from 'react-redux';
import { orderActions } from '../_actions';

const EmployeeOrdersPage = props => {
    const id = props.user.employeeProfile.id;
    const { loadOrdersByEmployee } = props;

    useEffect(() => {
        loadOrdersByEmployee(id);
    }, [ id, loadOrdersByEmployee ]);
    
    const orderRows = props.orders && props.orders.map(order =>
        <EmployeeOrderRow key={order.id} {...order}/>        
    );
    return (
        <LoadingContainer isLoading={props.isOrdersLoading}>
            <Card body>
                <h5>Emloyee orders</h5>
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
                        {orderRows && orderRows.length === 0 ? (
                            <tr><td colSpan="4">You haven't got orders yet.</td></tr>
                        ) : orderRows}
                    </tbody>
                </Table>
            </Card>
        </LoadingContainer>
    );
};

const mapStateToProps = state => {
    const { orders, isOrdersLoading } = state.order;
    const { user } = state.authentication;
    return {
        orders,
        isOrdersLoading,
        user
    };
};

const mapDispatchToProps = dispatch => ({
    loadOrdersByEmployee: employeeId => dispatch(orderActions.loadOrdersByEmployee(employeeId))
});

const connectedEmployeeOrdersPage = connect(mapStateToProps, mapDispatchToProps)(EmployeeOrdersPage);
export { connectedEmployeeOrdersPage as EmployeeOrdersPage };