import React, { useState, useEffect } from 'react';
import { LoadingContainer } from '../_components';
import { Card, Table, Button } from 'reactstrap';
import { RootState } from '../_store';
import { ThunkDispatch } from 'redux-thunk';
import { ServiceTypeState, ServiceTypeActionTypes } from '../_store/serviceType/types';
import * as serviceTypesActions from '../_store/serviceType/actions';
import { connect, ConnectedProps } from 'react-redux';
import { ServiceTypeRow } from './ServiceTypeRow';
import { ServiceTypeModal } from './ServiceTypeModal';

const mapState = (state: RootState) => ({
    serviceTypes: state.serviceType.list,
    listLoading: state.serviceType.listLoading
});

const mapDispatch = (
    dispatch: ThunkDispatch<ServiceTypeState, undefined, ServiceTypeActionTypes>
) => ({
    loadServicesList: () => dispatch(serviceTypesActions.getAllOrderedByProfilesCount())
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;

type ServiceTypesPageProps = PropsFromRedux & Readonly<{}>;

type ServiceTypesPageState = {
    isEditModalVisible: boolean;
    modalId?: number;
    modalName?: string;
};

const ServiceTypesPage = (props: ServiceTypesPageProps) => {
    const [state, setState] = useState<ServiceTypesPageState>({
        isEditModalVisible: false,
        modalId: undefined,
        modalName: undefined
    });

    const { loadServicesList } = props;
    useEffect(() => {
        loadServicesList();
    }, [loadServicesList])

    const openEditModal = (id?: number, name?: string) => {
        setState({isEditModalVisible: true, modalId: id, modalName: name});
    };

    const closeModal = () => setState({ isEditModalVisible: false });

    const serviceTypeRows = props.serviceTypes && props.serviceTypes.map(st => 
        <ServiceTypeRow key={st.id} id={st.id} name={st.name as string} openEditModal={openEditModal}></ServiceTypeRow>
    );

    return(
        <LoadingContainer isLoading={props.listLoading}>
            <ServiceTypeModal id={state.modalId} name={state.modalName} isOpen={state.isEditModalVisible} closeModal={closeModal}/>
            <Card body>
                <h3>Service types</h3>
                <Button onClick={() => {openEditModal()}}>Add service</Button>
                <Table className='mt-3 table-bordered'>
                    <thead>
                        <tr>
                            <th>Id</th>
                            <th>Name</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {serviceTypeRows}
                    </tbody>
                </Table>
            </Card>
        </LoadingContainer>
    );
};

const connectedServiceTypesPage = connector(ServiceTypesPage);
export { connectedServiceTypesPage as ServiceTypesPage };