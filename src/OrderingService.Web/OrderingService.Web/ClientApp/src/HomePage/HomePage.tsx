import React, { useEffect } from 'react';
import { LoadingContainer, PaginationBlock } from '../_components';
import { EmployeeItem } from './EmployeeItem';
import { SearchBlock } from './SearchBlock';
import { connect, ConnectedProps } from 'react-redux';
import { useParams, useLocation } from 'react-router-dom';
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
    loadEmployees: (pageNumber: number, searchString?: string, serviceTypeId?: number, maxServiceCost?: number) => 
        dispatch(employeeActions.loadList(pageNumber, searchString, serviceTypeId, maxServiceCost))
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type HomePageProps = PropsFromRedux & {};

const useQuery = () => {
    return new URLSearchParams(useLocation().search);
};

const HomePage = (props: HomePageProps) => {
    
    const { page } = useParams();
    const pageNumber = parseInt(page as string) || 1;

    const query = useQuery();
    const maxServiceCost = parseInt(query.get('maxServiceCost') || '');
    const serviceTypeId = parseInt(query.get('service') || '');
    const searchString = query.get('search');
    
    const { loadEmployees, pagesCount } = props;

    useEffect(() => {
        loadEmployees(
            pageNumber, 
            searchString || undefined,
            serviceTypeId || undefined,
            maxServiceCost || undefined);
    }, [loadEmployees, pageNumber, maxServiceCost, serviceTypeId, searchString]);
    
    const list = props.employeeList && props.employeeList.map(employee => 
        <EmployeeItem key={employee.id} employee={employee}/>
    );
    
    return (
        <>
        <SearchBlock />
        <LoadingContainer isLoading={props.listLoading}>
            {list}
            {!!pagesCount && (
                <PaginationBlock pageNumber={pageNumber} pagesCount={pagesCount} pathPrefix={''}/>
            )}
        </LoadingContainer>
        </>
    );
}

const connectedHomePage = connector(HomePage);
export { connectedHomePage as HomePage };