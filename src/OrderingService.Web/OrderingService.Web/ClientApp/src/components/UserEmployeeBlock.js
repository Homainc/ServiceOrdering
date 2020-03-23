import * as Yup from 'yup';
import React, { useState } from 'react';
import { Col, Row, ListGroupItem, ListGroupItemHeading, Button, ListGroupItemText } from 'reactstrap';
import { Formik, Form } from 'formik';
import { ValidationTextField } from './ValidationTextField';
import { LoadingButton } from './LoadingButton';
import { profileActions } from '../actions/profile.actions';
import { connect } from 'react-redux';

const UserEmployeeBlock = props => {

    const [state, setState] = useState({ editMode: false });

    const employeeProfile = props.profile && props.profile.employeeProfile;

    const handleProfileProcessed = () => {
        setState({ editMode: false });
    };

    const handleSubmit = values =>{
        const profile = {
            serviceType: values.serviceType,
            serviceCost: values.serviceCost,
            description: values.description
        };
        if(!employeeProfile)
            props.createEmployeeProfile(profile)
                .then(handleProfileProcessed);
        else
            props.updateEmployeeProfile(profile)
                .then(handleProfileProcessed);
    };

    return(
        <ListGroupItem>
            <Row>
                <Col><ListGroupItemHeading>Employee profile</ListGroupItemHeading></Col>
            </Row>
            {!employeeProfile && !state.editMode && (<>
            <Row><ListGroupItemText className="p-3">You didn't create a employee profile</ListGroupItemText></Row>
            <Row><Button 
                color="link"
                onClick={() => setState({ editMode: true })}>Create the employee profile</Button></Row>
            </>)}
            {!state.editMode && employeeProfile && (<>
            <ListGroupItemHeading>Service type</ListGroupItemHeading>
            <ListGroupItemText>{employeeProfile && employeeProfile.serviceType}</ListGroupItemText>
            <ListGroupItemHeading>Service cost</ListGroupItemHeading>
            <ListGroupItemText>{employeeProfile && employeeProfile.serviceCost}</ListGroupItemText>
            </>)}
            {state.editMode && (
                <Formik
                    initialValues={employeeProfile || {
                        serviceType: '',
                        serviceCost: '',
                        description: ''
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
                            label="Service Type"/>
                        <ValidationTextField 
                            id="serviceCost" 
                            name="serviceCost" 
                            label="Service Cost"/>
                        <ValidationTextField 
                            id="description" 
                            name="description" 
                            label="Description"/>
                        <LoadingButton type="submit" color="success">Save</LoadingButton>
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
        </ListGroupItem>
    );
};

const mapStateToProps = state => {
    const { profileProcessing } = state.profile;
    return {
        profileProcessing
    };
};

const mapDispatchToProps = dispatch => ({
    createEmployeeProfile: employeeProfile => dispatch(profileActions.createEmployeeProfile(employeeProfile)),
    updateEmployeeProfile: employeeProfile => dispatch(profileActions.updateEmployeeProfile(employeeProfile))
});

const connectedUserEmployeeBlock = connect(mapStateToProps, mapDispatchToProps)(UserEmployeeBlock);
export { connectedUserEmployeeBlock as UserEmployeeBlock };