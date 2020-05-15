import * as Yup from 'yup';
import React, { useState } from 'react';
import { Col, Row, ListGroupItemHeading, Button, ListGroupItemText, Spinner } from 'reactstrap';
import { Formik, Form, FormikHelpers } from 'formik';
import { LoadingButton, ValidationTextField } from '../_components';
import { connect, ConnectedProps } from 'react-redux';
import { RootState } from '../_store';
import { ThunkDispatch } from 'redux-thunk';
import { EmployeeActionTypes } from '../_store/employee/types';
import { EmployeeProfileCreateDto, EmployeeProfileUpdateDto } from '../WebApiModels';
import * as employeeActions from '../_store/employee/actions';

const mapState = (state: RootState) => ({
    employeeProcessing: state.employee.creating || state.employee.updating,
    employeeDeleting: state.employee.deleting,
    employeeProfile: state.employee.employee,
    user: state.auth.user
});

const mapDispatch = (
    dispatch: ThunkDispatch<RootState, undefined, EmployeeActionTypes>
) => ({
    createEmployeeProfile: async (employee: EmployeeProfileCreateDto) => dispatch(employeeActions.create(employee)),
    updateEmployeeProfile: async (employee: EmployeeProfileUpdateDto) => dispatch(employeeActions.update(employee)),
    deleteEmployeeProfile: (id: string) => dispatch(employeeActions.deleteById(id)),
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type UserEmployeeBlockProps = PropsFromRedux & Readonly<{}>;

type FormikValues = {
    serviceType: string;
    serviceCost: React.ReactText;
    description: string;
};

const UserEmployeeBlock = (props: UserEmployeeBlockProps) => {

    const [state, setState] = useState({ editMode: false });

    const employeeProfile = props.employeeProfile;

    const handleProfileProcessed = () => {
        setState({ editMode: false });
    };

    const handleSubmit = (values: FormikValues, { setErrors }: FormikHelpers<FormikValues>) => {
        const profile: EmployeeProfileCreateDto = {
            serviceType: values.serviceType,
            serviceCost: values.serviceCost as number,
            description: values.description,
            userId: props.user?.id || ''
        };
        if(!employeeProfile)
            props.createEmployeeProfile(profile)
                .then(handleProfileProcessed)
                .catch(errors => setErrors(errors));
        else
            props.updateEmployeeProfile({...profile, id: employeeProfile.id})
                .then(handleProfileProcessed)
                .catch(errors => setErrors(errors));
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
            <ListGroupItemText className="text-secondary">{employeeProfile.serviceType}</ListGroupItemText>
            <ListGroupItemHeading>Service cost</ListGroupItemHeading>
            <ListGroupItemText className="text-secondary">{employeeProfile.serviceCost}</ListGroupItemText>
            <ListGroupItemHeading>Description</ListGroupItemHeading>
            <ListGroupItemText className="text-secondary">{employeeProfile.description}</ListGroupItemText>
            {employeeProfile && !state.editMode && (<>
            <Spinner className={props.employeeDeleting?'':'collapse'} size="sm" color="danger"/>
            <Row><Button color="link"
                className="text-danger" 
                onClick={() => props.deleteEmployeeProfile(employeeProfile.id as string)} 
                disabled={props.employeeDeleting}>Delete employee profile</Button></Row></>)}
            </>)}
            {state.editMode && (
                <Formik
                    initialValues={{
                        serviceType: (employeeProfile?.serviceType) || '',
                        serviceCost: (employeeProfile?.serviceCost) || '',
                        description: (employeeProfile?.description) || ''
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

const connectedUserEmployeeBlock = connector(UserEmployeeBlock);
export { connectedUserEmployeeBlock as UserEmployeeBlock };