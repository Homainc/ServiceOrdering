import React, { useEffect, SyntheticEvent, useState } from 'react';
import { RootState } from '../_store';
import { ThunkDispatch } from 'redux-thunk';
import { connect, ConnectedProps } from 'react-redux';
import { Card, Form, Input, FormGroup, Row, Col } from 'reactstrap';
import { ServiceTypeState, ServiceTypeActionTypes } from '../_store/serviceType/types';
import * as serviceTypes from '../_store/serviceType/actions';
import { history } from '../_helpers';
import _ from 'lodash';

type SearchBlockState = {
    searchString: string,
    serviceTypeId?: number,
    maxServiceCost?: number,
};

const mapState = (state: RootState) => ({
    servicesLoading: state.serviceType.listLoading,
    servicesList: state.serviceType.list
});

const mapDispatch = (
    dispatch: ThunkDispatch<ServiceTypeState, undefined, ServiceTypeActionTypes>
) => ({
    getAllServices: async () => await dispatch(serviceTypes.getAllOrderedByProfilesCount()) 
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type SearchBlockProps = PropsFromRedux & Readonly<{
    initialValues: SearchBlockState
}>;

const SearchBlock = (props: SearchBlockProps) => {
    const [state, setState] = useState<SearchBlockState>(props.initialValues);

    const updateQueryParams = (searchString: string, serviceTypeId?: number, maxServiceCost?: number) => {
        let queryParams = searchString? `?search=${searchString}` : '';
        queryParams += serviceTypeId? (queryParams? '&': '?') + 
            `service=${serviceTypeId}` : ''; 
        queryParams += maxServiceCost? (queryParams? '&': '?') +
            `maxServiceCost=${maxServiceCost}` : '';
        queryParams = '/page/1' + queryParams;

        history.replace(queryParams);
    };

    const [ debouncedUpdateQueryParams ] = useState(() => _.debounce(updateQueryParams, 250));

    const { getAllServices } = props;
    useEffect(() => {
        getAllServices();
    }, [ getAllServices ]);

    const handleSearchInputChange = ({ target }: SyntheticEvent) => {
        const input = target as HTMLInputElement;
        setState({ ...state, searchString: input.value });
        debouncedUpdateQueryParams(input.value, state.serviceTypeId, state.maxServiceCost);
    };

    const handleCategoryChange = ({ target }: SyntheticEvent) => {
        const select = target as HTMLSelectElement;
        setState({...state, serviceTypeId: parseInt(select.value)});
        debouncedUpdateQueryParams(state.searchString, parseInt(select.value), state.maxServiceCost);
    };

    const handleMaxServiceCostChange = ({ target }: SyntheticEvent) => {
        const input = target as HTMLInputElement;
        setState({...state, maxServiceCost: parseInt(input.value)});
        debouncedUpdateQueryParams(state.searchString, state.serviceTypeId, parseInt(input.value));
    };

    const servicesList = props.servicesList && props.servicesList.map(service => 
        <option key={service.id} value={service.id}>
            {service.name}
        </option>
    );

    return (
        <>
        <Card body className="mb-4">
            <h5>Filter</h5>
            <Form>
                <Row>
                    <Col cols={6}>
                    <FormGroup>
                            <Input 
                                type='select' 
                                className='custom-select' 
                                disabled={props.servicesLoading} 
                                onChange={handleCategoryChange}
                                value={isNaN(state.serviceTypeId || NaN)? undefined: state.serviceTypeId}
                                placeholder={'Choose service'}>
                                    <option>Choose service...</option>
                                    {servicesList}
                            </Input>
                        </FormGroup>
                    </Col>
                    <Col>
                        <FormGroup>
                            <Input 
                                type='number' 
                                value={isNaN(state.maxServiceCost || NaN)? '': state.maxServiceCost}
                                onChange={handleMaxServiceCostChange}
                                placeholder={'Maximum cost'}/>
                        </FormGroup>
                    </Col>
                </Row>
                <FormGroup>
                    <Input 
                        type='text' 
                        onChange={handleSearchInputChange}
                        value={state.searchString}
                        placeholder={'Search string'}/> 
                </FormGroup>
            </Form>
        </Card>
        </>
    );
};

const connectedSearchBlock = connector(SearchBlock);

export { connectedSearchBlock as SearchBlock };