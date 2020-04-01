import React, { useEffect } from 'react';
import { LoadingContainer, PaginationBlock } from '../_components';
import { EmployeeItem } from './EmployeeItem';
import { employeeActions } from '../_actions';
import { connect } from 'react-redux';
import { useParams } from 'react-router-dom';

const HomePage = props => {
    
    const { page } = useParams();
    const pageNumber = parseInt(page) || 1;
    const { loadEmployees, pagesCount } = props;

    useEffect(() => {
        loadEmployees(pageNumber);
    }, [loadEmployees, pageNumber]);
    
    const list = props.employeeList && props.employeeList.map(employee => 
        <EmployeeItem key={employee.id} employee={employee}/>
    );
    
    return (
        <LoadingContainer isLoading={props.listLoading}>
            {list}
            {!!pagesCount && (
                <PaginationBlock pageNumber={pageNumber} pagesCount={pagesCount}/>
            )}
        </LoadingContainer>
    );
}

const mapStateToProps = state => {
    const { employeeList, listLoading, pagesCount } = state.employee;
    return {
        employeeList,
        listLoading,
        pagesCount
    };
};

const mapDispatchToProps = dispatch => ({
    loadEmployees: (pageNumber) => dispatch(employeeActions.loadEmployees(pageNumber))
});

const connectedHomePage = connect(mapStateToProps, mapDispatchToProps)(HomePage);
export { connectedHomePage as HomePage };