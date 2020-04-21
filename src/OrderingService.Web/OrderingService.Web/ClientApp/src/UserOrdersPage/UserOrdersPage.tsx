import React, { useEffect } from 'react';
import { connect, ConnectedProps } from 'react-redux';
import { LoadingContainer, PaginationBlock } from '../_components';
import { useParams } from 'react-router-dom';
import { UserOrderRow } from './UserOrderRow';
import { ReviewModal } from './ReviewModal';
import { Table, Card } from 'reactstrap';
import { RootState } from '../_store';
import { ThunkDispatch } from 'redux-thunk';
import { OrderActionTypes, OrderState } from '../_store/order/types';
import * as orderActions from '../_store/order/actions';

const mapState = (state: RootState) => ({
    user: state.auth.user,
    orders: state.order.orders,
    pagesCount: state.order.pagesCount,
    ordersLoading: state.order.listLoading,
    totalOrders: state.order.totalOrders
});

const mapDispatch = (
    dispatch: ThunkDispatch<OrderState, undefined, OrderActionTypes>
) => ({
    loadOrdersByUser: (userId: string, pageNumber: number) => dispatch(orderActions.loadOrdersByUser(userId, pageNumber))
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type UserOrdersPageProps = PropsFromRedux & Readonly<{}>;

const UserOrdersPage = (props: UserOrdersPageProps) => {
    const { loadOrdersByUser, user } = props;
    const { page } = useParams();
    const pageNumber = parseInt(page as string) || 1;

    useEffect(() => {
        loadOrdersByUser(user?.id as string, pageNumber);
    }, [user, loadOrdersByUser, pageNumber]);

    const ordersRows = props.orders && props.orders.map(order => 
        <UserOrderRow 
            key={order.id}
            id={order.id}
            date={order.date}
            status={order.status}
            briefTask={order.briefTask}
            serviceDetails={order.serviceDetails}
            price={order.price}
            address={order.address}
            clientId={order.clientId}
            employeeId={order.employeeId}/>
    );
    return(
        <LoadingContainer isLoading={props.ordersLoading}>
            <Card body>
                <h5>My orders ({props.totalOrders || 0})</h5>
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
                            <tr><td colSpan={4}>You haven't made orders yet.</td></tr>
                        ) : ordersRows}
                    </tbody>
                </Table>
                {!!props.pagesCount && (
                    <PaginationBlock pageNumber={pageNumber} pagesCount={props.pagesCount} pathPrefix={'/orders'}/>
                )}
                <ReviewModal /> 
            </Card>
        </LoadingContainer>
    );
};

const connectedUserOrdersPage = connector(UserOrdersPage);
export { connectedUserOrdersPage as UserOrdersPage };