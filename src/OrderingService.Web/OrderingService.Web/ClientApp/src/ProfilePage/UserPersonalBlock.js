import * as Yup from 'yup';
import React, { useState } from 'react';
import { Col, Row, ListGroupItemHeading, ListGroupItemText, Button, Alert } from 'reactstrap';
import { Formik, Form } from 'formik';
import { LoadingButton, ImageUpload, ValidationTextField } from '../_components';
import { profileActions } from '../_actions/profile.actions';
import { connect } from 'react-redux';

const UserPersonalBlock = props => {
    const [state, setState] = useState({ editMode: false });
    const { profileUpdating } = props;

    const handleProfileUpdated = () => {
        setState({ editMode: false });
    };
    const { profile } = props;

    return(
        <>
            <Row>
                <Col><ListGroupItemHeading className="p-1 pb-1">Personal data</ListGroupItemHeading></Col>
                <Col sm="5" md="4" lg="3" className="d-flex">
                <Button 
                    color="link" 
                    className={state.editMode ? 'collapse': '' } 
                    onClick={()=> setState({ editMode: true })}>
                        Edit personal data
                </Button>
                </Col>
            </Row>
            <Formik
                enableReinitialize={true}
                initialValues={{
                    imageUrl: (profile && profile.imageUrl) || '',
                    lastName: (profile && profile.lastName) || '',
                    firstName: (profile && profile.firstName) || '',
                    phoneNumber: (profile && profile.phoneNumber) || ''
                }}
                validationSchema={Yup.object({
                    firstName: Yup.string()
                        .required("First name is required")
                        .max(20, "First name must be at most 20 characters"),
                    lastName: Yup.string()
                        .required("Last name is required")
                        .max(20, "Last name must be at most 20 characters"),
                    phoneNumber: Yup.string()
                        .matches(/^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s./0-9]*$/, "Incorrect phone number")
                        .notRequired()
                })}
                onSubmit={(values) => {
                    props.updateProfile({
                        ...props.profile,
                        lastName: values.lastName,
                        firstName: values.firstName,
                        phoneNumber: values.phoneNumber,
                        imageUrl: values.imageUrl
                    }).then(handleProfileUpdated);
                }}>
                {({ resetForm }) => (
                <Form><Row>
                    <Col sm="5" md="4" lg="3">
                        <ImageUpload id="imageUrl" name="imageUrl" disabled={!state.editMode}/>
                    </Col>
                    <Col>
                        {!state.editMode ? (<>
                            <ListGroupItemHeading>First name</ListGroupItemHeading>
                            <ListGroupItemText className="text-secondary">{props.profile && props.profile.firstName}</ListGroupItemText>
                            <ListGroupItemHeading>Last name</ListGroupItemHeading>
                            <ListGroupItemText className="text-secondary">{props.profile && props.profile.lastName}</ListGroupItemText>
                            <ListGroupItemHeading>Phone number</ListGroupItemHeading>
                            <ListGroupItemText className="text-secondary">{(props.profile && props.profile.phoneNumber) || 'Not specified'}</ListGroupItemText>
                        </>):(<>
                            <Alert isOpen={!!props.alert.message} color={props.alert.type}>{props.alert.message}</Alert>
                            <ValidationTextField 
                                className="form-control-sm" 
                                label="First Name" 
                                id="firstName" 
                                name="firstName"
                                disabled={profileUpdating}></ValidationTextField>
                            <ValidationTextField 
                                className="form-control-sm" 
                                label="Last Name" 
                                id="lastName" 
                                name="lastName"
                                disabled={profileUpdating}></ValidationTextField>
                            <ValidationTextField 
                                className="form-control-sm" 
                                label="Phone number" 
                                id="phoneNumber" 
                                name="phoneNumber"
                                disabled={profileUpdating}></ValidationTextField>
                            <LoadingButton type="submit" isLoading={profileUpdating} color="success" className="btn-sm">Save</LoadingButton>
                            <Button className="btn-sm ml-1" onClick={() => {
                                resetForm();
                                setState({ editMode: false });
                            }}>Cancel</Button>
                        </>)}
                    </Col>
                </Row></Form>
                )}
            </Formik>
        </>
    );
};

const mapStateToProps = state => {
    const { alert } = state;
    const { profileUpdating } = state.profile;
    return {
        alert,
        profileUpdating
    };
};

const mapDispatchToProps = dispatch => ({
    updateProfile: profile => dispatch(profileActions.updateProfile(profile))
});

const connectedUserPersonalBlock = connect(mapStateToProps, mapDispatchToProps)(UserPersonalBlock);
export { connectedUserPersonalBlock as UserPersonalBlock };