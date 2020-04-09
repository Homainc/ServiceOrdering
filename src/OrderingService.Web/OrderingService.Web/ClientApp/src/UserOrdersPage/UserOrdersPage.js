import React, { useEffect } from 'react';
import { orderActions } from '../_actions';
import { connect } from 'react-redux';
import { LoadingContainer, PaginationBlock } from '../_components';
import { useParams } from 'react-router-dom';
import { UserOrderRow } from './UserOrderRow';
import { ReviewModal } from './ReviewModal';
import { Table, Card } from 'reactstrap';

const UserOrdersPage = props => {
    const { loadOrdersByUser, user } = props;
    const { page } = useParams();
    const pageNumber = parseInt(page) || 1;

    useEffect(() => {
        loadOrdersByUser(user.id, pageNumber);
    }, [user, loadOrdersByUser, pageNumber]);

    const ordersRows = props.orders && props.orders.map(order => 
        <UserOrderRow key={order.id} {...order}/>
    );
    return(
        <LoadingContainer isLoading={props.isOrdersLoading}>
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
                            <tr><td colSpan="4">You haven't made orders yet.</td></tr>
                        ) : ordersRows}
                    </tbody>
                </Table>
                <PaginationBlock pageNumber={pageNumber} pagesCount={props.pagesCount} pathPrefix={'/orders'}/>
                <ReviewModal /> 
            </Card>
        </LoadingContainer>
    );
};

const mapStateToProps = state => {
    const { user } = state.authentication;
    const { orders, isOrdersLoading, pagesCount, totalOrders } = state.order;
    return {
        user,
        orders,
        pagesCount,
        isOrdersLoading,
        totalOrders
    };
}

const mapDispatchToProps = dispatch => ({
    loadOrdersByUser: (userId, pageNumber) => dispatch(orderActions.loadOrdersByUser(userId, pageNumber))
});

const connectedUserOrdersPage = connect(mapStateToProps, mapDispatchToProps)(UserOrdersPage);
export { connectedUserOrdersPage as UserOrdersPage };