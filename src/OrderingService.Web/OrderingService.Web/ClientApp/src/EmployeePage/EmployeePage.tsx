import React, { useEffect } from 'react';
import { useParams } from 'react-router-dom';
import { Card, Row, Col } from 'reactstrap';
import { LoadingContainer, EmployeeOverview } from '../_components';
import { ReviewsBlock } from './ReviewsBlock';
import { connect, ConnectedProps } from 'react-redux';
import '@fortawesome/fontawesome-free/css/all.css';
import { RootState } from '../_store';
import { EmployeeActionTypes } from '../_store/employee/types';
import { ThunkDispatch } from 'redux-thunk';
import * as employeeActions from '../_store/employee/actions';

const mapState = (state: RootState) => ({
    employeeProfileLoading: state.employee.loading,
    employeeProfile: state.employee.employee
});

const mapDispatch = (
    dispatch: ThunkDispatch<RootState, undefined, EmployeeActionTypes>
) => ({
    loadEmployeeProfile: (id: string) => dispatch(employeeActions.load(id))
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type EmployeePageProps = PropsFromRedux & {
    employeeId: string;
};

const EmployeePage = (props: EmployeePageProps) => {

    const { id } = useParams();
    const { loadEmployeeProfile } = props;

    useEffect(() => {
        loadEmployeeProfile(id as string);
    }, [ id, loadEmployeeProfile ]);

    const { employeeProfile } = props;

    return(
        <LoadingContainer isLoading={props.employeeProfileLoading}>
            <Card body className="bg-light">
                <EmployeeOverview withHireButton={true} employee={employeeProfile}/>
                <hr/>
                <Row>
                    <Col>
                    <h5>Description</h5>
                    <p className="text-secondary" >{employeeProfile && employeeProfile.description}</p>
                    </Col>
                </Row>
                <ReviewsBlock employeeId={employeeProfile?.id}/>
            </Card>
        </LoadingContainer>
    );
};

const connectedEmployeePage = connector(EmployeePage);
export { connectedEmployeePage as EmployeePage };