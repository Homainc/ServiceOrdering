import React, { useEffect, SyntheticEvent, useState, useCallback } from 'react';
import { RootState } from '../_store';
import { ThunkDispatch } from 'redux-thunk';
import { connect, ConnectedProps } from 'react-redux';
import { Card, Form, Input, FormGroup, Label, Row, Col } from 'reactstrap';
import { ServiceTypeState, ServiceTypeActionTypes } from '../_store/serviceType/types';
import * as serviceTypes from '../_store/serviceType/actions';
import { history } from '../_helpers';
import _ from 'lodash';
import { useParams } from 'react-router';

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
type SearchBlockProps = PropsFromRedux & Readonly<{}>;

type SearchBlockState = {
    searchString: string,
    serviceTypeId: number | undefined,
    maxServiceCost: number | undefined,
};

const SearchBlock = (props: SearchBlockProps) => {
    const { page } = useParams();
    
    const [state, setState] = useState<SearchBlockState>({
        searchString: '',
        serviceTypeId: undefined,
        maxServiceCost: undefined
    });

    const changeState = _.debounce(setState, 1000);
    const handleChange = useCallback(changeState, []);

    const { getAllServices } = props;

    useEffect(() => {
        getAllServices();
    }, [ getAllServices ]);

    useEffect(() => {
        let queryParams: string = `/page/1`;
        queryParams += state.searchString? `?search=${state.searchString}` : '';
        queryParams += state.serviceTypeId? (queryParams? '&': '?') + 
            `service=${state.serviceTypeId}` : ''; 
        queryParams += state.maxServiceCost? (queryParams? '&': '?') +
            `maxServiceCost=${state.maxServiceCost}` : '';

        history.replace(queryParams);
    }, [state]);

    const handleSearchInputChange = ({ target }: SyntheticEvent) => {
        const input = target as HTMLInputElement;
        handleChange({ ...state, searchString: input.value });
    };

    const handleCategoryChange = ({ target }: SyntheticEvent) => {
        const select = target as HTMLSelectElement;
        handleChange({...state, serviceTypeId: parseInt(select.value)});
    };

    const handleMaxServiceCostChange = ({ target }: SyntheticEvent) => {
        const input = target as HTMLInputElement;
        handleChange({...state, maxServiceCost: parseInt(input.value)});
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
                            <Label>Category</Label>
                            <Input type='select' className='custom-select' disabled={props.servicesLoading} onChange={handleCategoryChange}>
                                {servicesList}
                            </Input>
                        </FormGroup>
                    </Col>
                    <Col>
                        <FormGroup>
                            <Label>Maximum price</Label>
                            <Input type='number' onChange={handleMaxServiceCostChange}/>
                        </FormGroup>
                    </Col>
                </Row>
                <FormGroup>
                    <Label>Search</Label>
                    <Input type='text' onChange={handleSearchInputChange}/> 
                </FormGroup>
            </Form>
        </Card>
        </>
    );
};

const connectedSearchBlock = connector(SearchBlock);

export { connectedSearchBlock as SearchBlock };