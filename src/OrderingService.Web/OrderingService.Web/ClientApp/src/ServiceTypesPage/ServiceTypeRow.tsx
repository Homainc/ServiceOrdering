import React from 'react';
import { Button } from 'reactstrap';
import { connect, ConnectedProps } from 'react-redux';
import { RootState } from '../_store';
import { ThunkDispatch } from 'redux-thunk';
import * as serviceTypeActions from '../_store/serviceType/actions';
import { ServiceTypeActionTypes } from '../_store/serviceType/types';


const mapState = (state: RootState) => ({
    isProcessing: state.serviceType.isServiceProcessing
});

const mapDispatch = (
    dispatch: ThunkDispatch<RootState, undefined, ServiceTypeActionTypes>
) => ({
    delete: async (id: number) => dispatch(serviceTypeActions.deleteByid(id))
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;

type ServiceTypeRowProps = PropsFromRedux & Readonly<{
    id: number;
    name: string;
    openEditModal: (id?:  number, name?: string) => void;
}>;

const ServiceTypeRow = (props : ServiceTypeRowProps) => {

    return(
        <tr>
            <td>{props.id}</td>
            <td>{props.name}</td>
            <td>
                <Button 
                    disabled={props.isProcessing}
                    color='link' 
                    className="text-primary p-0 " 
                    onClick={() => props.openEditModal(props.id, props.name)}>
                        Edit
                </Button>
                <Button 
                    disabled={props.isProcessing}
                    color='link' 
                    className="text-danger p-0 ml-2" 
                    onClick={() => props.delete(props.id)}>
                        Delete
                </Button>
            </td>
        </tr>
    );
};

const connectedServiceTypeRow = connector(ServiceTypeRow);
export { connectedServiceTypeRow as ServiceTypeRow };