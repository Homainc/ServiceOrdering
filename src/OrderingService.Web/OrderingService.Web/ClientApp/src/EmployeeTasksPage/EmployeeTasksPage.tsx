import React, { useEffect } from 'react';
import { Card, Table } from 'reactstrap';
import { LoadingContainer, PaginationBlock } from '../_components';
import { EmployeeOrderRow } from './EmployeeOrderRow';
import { connect, ConnectedProps } from 'react-redux';
import { useParams } from 'react-router-dom';
import { RootState } from '../_store';
import * as orderActions from '../_store/order/actions';
import { OrderState, OrderActionTypes } from '../_store/order/types';
import { ThunkDispatch } from 'redux-thunk';

const mapState = (state: RootState) => ({
    orders: state.order.orders,
    ordersLoading: state.order.listLoading,
    user: state.auth.user,
    pagesCount: state.order.pagesCount,
    totalOrders: state.order.totalOrders
});

const mapDispatch = (
    dispatch: ThunkDispatch<OrderState, undefined, OrderActionTypes>
) => ({
    loadOrdersByEmployee: (employeeId: string, pageNumber: number) => dispatch(orderActions.loadOrdersByEmployee(employeeId, pageNumber))
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type EmployeeOrdersPageProps = PropsFromRedux & Readonly<{
    status: number;
    orderId: number;
}>;

const EmployeeOrdersPage = (props: EmployeeOrdersPageProps) => {
    const id = props.user?.employeeProfile?.id;
    const { page } = useParams();
    const pageNumber = parseInt(page as string) || 1;
    const { loadOrdersByEmployee } = props;

    useEffect(() => {
        if(id)
            loadOrdersByEmployee(id, pageNumber);
    }, [ id, loadOrdersByEmployee, pageNumber ]);
    
    const orderRows = props.orders && props.orders.map(order =>
        <EmployeeOrderRow key={order.id}
            id={order.id}
            date={order.date}
            briefTask={order.briefTask}
            serviceDetails={order.serviceDetails}
            price={order.price}
            address={order.address}
            contactPhone={order.contactPhone}
            status={order.status}/>        
    );
    return (
        <LoadingContainer isLoading={props.ordersLoading}>
            <Card body>
                <h5>Tasks ({props.totalOrders || 0})</h5>
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
                            <tr><td colSpan={4}>You haven't got orders yet.</td></tr>
                        ) : orderRows}
                    </tbody>
                </Table>
                {!!props.pagesCount && (
                    <PaginationBlock pageNumber={pageNumber} pagesCount={props.pagesCount} pathPrefix={'/tasks'}/>
                )}
            </Card>
        </LoadingContainer>
    );
};

const connectedEmployeeOrdersPage = connector(EmployeeOrdersPage);
export { connectedEmployeeOrdersPage as EmployeeOrdersPage };