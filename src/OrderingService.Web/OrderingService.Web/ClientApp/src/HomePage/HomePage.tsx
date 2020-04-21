import React, { useEffect } from 'react';
import { LoadingContainer, PaginationBlock } from '../_components';
import { EmployeeItem } from './EmployeeItem';
import { connect, ConnectedProps } from 'react-redux';
import { useParams } from 'react-router-dom';
import { RootState } from '../_store';
import { ThunkDispatch } from 'redux-thunk';
import { EmployeeState, EmployeeActionTypes } from '../_store/employee/types';
import * as employeeActions from '../_store/employee/actions';

const mapState = (state: RootState) => ({
    employeeList: state.employee.list,
    pagesCount: state.employee.pagesCount,
    listLoading: state.employee.listLoading
});
const mapDispatch = (
    dispatch: ThunkDispatch<EmployeeState, undefined, EmployeeActionTypes>
) => ({
    loadEmployees: (pageNumber: number) => dispatch(employeeActions.loadList(pageNumber))
});
const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type HomePageProps = PropsFromRedux & {};

const HomePage = (props: HomePageProps) => {
    
    const { page } = useParams();
    const pageNumber = parseInt(page as string) || 1;
    const { loadEmployees, pagesCount } = props;

    useEffect(() => {
        props.loadEmployees(pageNumber);
    }, [loadEmployees, pageNumber]);
    
    const list = props.employeeList && props.employeeList.map(employee => 
        <EmployeeItem key={employee.id} employee={employee}/>
    );
    
    return (
        <LoadingContainer isLoading={props.listLoading}>
            {list}
            {!!pagesCount && (
                <PaginationBlock pageNumber={pageNumber} pagesCount={pagesCount} pathPrefix={''}/>
            )}
        </LoadingContainer>
    );
}

const connectedHomePage = connector(HomePage);
export { connectedHomePage as HomePage };