import * as Yup from 'yup';
import React, { useState } from 'react';
import { Col, Row, ListGroupItemHeading, Button, ListGroupItemText, Spinner } from 'reactstrap';
import { Formik, Form } from 'formik';
import { LoadingButton, ValidationTextField } from '../_components';
import { employeeActions, profileActions } from '../_actions';
import { connect } from 'react-redux';

const UserEmployeeBlock = props => {

    const [state, setState] = useState({ editMode: false });

    const employeeProfile = props.employeeProfile;

    const handleProfileProcessed = () => {
        setState({ editMode: false });
    };

    const handleSubmit = values =>{
        const profile = {
            serviceType: values.serviceType,
            serviceCost: values.serviceCost,
            description: values.description,
            userId: props.user.id
        };
        if(!employeeProfile)
            props.createEmployeeProfile(profile)
                .then(handleProfileProcessed);
        else
            props.updateEmployeeProfile({...profile, id: employeeProfile.id})
                .then(handleProfileProcessed);
    };

    return(
        <>
            <Row>
                <Col className="py-2"><ListGroupItemHeading>Employee profile</ListGroupItemHeading></Col>
                {!state.editMode && employeeProfile && (
                <Col sm="5" md="4" lg="3"><Button color="link" onClick={() => setState({ editMode: true })}>Edit employee profile</Button></Col>)}
            </Row>
            {!employeeProfile && !state.editMode && (<>
            <Row><ListGroupItemText className="p-3">You didn't create a employee profile</ListGroupItemText></Row>
            <Row><Button 
                color="link"
                onClick={() => setState({ editMode: true })}>Create the employee profile</Button></Row>
            </>)}
            {!state.editMode && employeeProfile && (<>
            <ListGroupItemHeading>Service type</ListGroupItemHeading>
            <ListGroupItemText className="text-secondary">{employeeProfile && employeeProfile.serviceType}</ListGroupItemText>
            <ListGroupItemHeading>Service cost</ListGroupItemHeading>
            <ListGroupItemText className="text-secondary">{employeeProfile && employeeProfile.serviceCost}</ListGroupItemText>
            <ListGroupItemHeading>Description</ListGroupItemHeading>
            <ListGroupItemText className="text-secondary">{employeeProfile && employeeProfile.description}</ListGroupItemText>
            {employeeProfile && !state.editMode && (<>
            <Spinner className={props.employeeDeleting?'':'collapse'} size="sm" color="danger"/>
            <Row><Button color="link"
                className="text-danger" 
                onClick={() => props.deleteEmployeeProfile(employeeProfile)} 
                disabled={props.employeeDeleting}>Delete employee profile</Button></Row></>)}
            </>)}
            {state.editMode && (
                <Formik
                    initialValues={{
                        serviceType: (employeeProfile && employeeProfile.serviceType) || '',
                        serviceCost: (employeeProfile && employeeProfile.serviceCost) || '',
                        description: (employeeProfile && employeeProfile.description) || ''
                    }}
                    validationSchema={Yup.object({
                        serviceType: Yup.string()
                            .required("Service type is required")
                            .max(20, "Service type must be at most 20 characters"),
                        serviceCost: Yup.number().typeError("Service cost must be a number")
                            .required("Service cost is required")
                            .positive("Service cost must be a positive number"),
                        description: Yup.string()
                    })}
                    onSubmit={handleSubmit}>
                    {({ resetForm }) => (
                    <Form>
                        <ValidationTextField 
                            id="serviceType"
                            name="serviceType" 
                            label="Service Type"
                            disabled={props.employeeProcessing}/>
                        <ValidationTextField 
                            id="serviceCost" 
                            name="serviceCost" 
                            label="Service Cost"
                            disabled={props.employeeProcessing}/>
                        <ValidationTextField 
                            id="description" 
                            name="description" 
                            label="Description"
                            type="textarea"
                            disabled={props.employeeProcessing}/>
                        <LoadingButton type="submit" color="success" isLoading={props.employeeProcessing}>Save</LoadingButton>
                        <Button
                            className="ml-1" 
                            onClick={() => {
                                resetForm();
                                setState({ editMode: false })
                            }}>Cancel</Button>
                    </Form>
                    )}
                </Formik>
            )}
        </>
    );
};

const mapStateToProps = state => {
    const { employeeProfile, employeeProcessing, employeeDeleting } = state.employee;
    const { user } = state.authentication;
    return {
        employeeProcessing,
        employeeDeleting,
        employeeProfile,
        user
    };
};

const mapDispatchToProps = dispatch => ({
    createEmployeeProfile: employeeProfile => dispatch(employeeActions.createEmployeeProfile(employeeProfile)),
    updateEmployeeProfile: employeeProfile => dispatch(employeeActions.updateEmployeeProfile(employeeProfile)),
    deleteEmployeeProfile: employeeProfile => dispatch(employeeActions.deleteEmployeeProfile(employeeProfile)),
    loadProfile: () => dispatch(profileActions.loadProfile())
});

const connectedUserEmployeeBlock = connect(mapStateToProps, mapDispatchToProps)(UserEmployeeBlock);
export { connectedUserEmployeeBlock as UserEmployeeBlock };