import React from 'react';
import { Card, Row, Col, CardTitle } from 'reactstrap';
import { Formik, Form } from 'formik';
import * as Yup from 'yup';
import { ValidationTextField, LoadingButton, ImageUpload } from '../_components';
import { connect, ConnectedProps } from 'react-redux';
import { RootState } from '../_store';
import { AuthActionTypes } from '../_store/auth/types';
import { ThunkDispatch } from 'redux-thunk';
import * as authActions from '../_store/auth/actions'; 
import { UserCreateDto } from '../WebApiModels';

const mapState = (state: RootState) => ({
    signingUp: state.auth.signingUp
});

const mapDispatch = (
    dispatch: ThunkDispatch<RootState, undefined, AuthActionTypes>
) => ({
    signUp: async (user: UserCreateDto) => dispatch(authActions.signUp(user))
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type SignUpPageProps = PropsFromRedux & Readonly<{}>;


const SignUpPage = (props: SignUpPageProps) => {
    const { signUp, signingUp } = props;
    return(
        <Row className="d-flex justify-content-center">
            <Col md="8">
                <Card body>
                    <Formik
                        initialValues={{
                            email: '',
                            password: '',
                            confirmPassword: '',
                            firstName: '',
                            lastName: '',
                            phoneNumber: '',
                            imagePublicId: 'default-employee'
                        }}
                        validationSchema={Yup.object({
                             email: Yup.string()
                                .required("Email is required")
                                .email("Incorrect email"),
                            password: Yup.string()
                                .required("Password is required")
                                .min(8, "Password length must contain at least 8 symbols")
                                .matches(/(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$.,/\\!%*?&()<>=_+])[A-Za-z\d@$.,/\\!%*?&()<>=_+]{8,}/, 
                                "Passwords must have at least one special character (e.g. '#', '@' and etc.), one uppercase letter, one lowercase letter, one number"),
                            confirmPassword: Yup.string()
                                .oneOf([Yup.ref('password'), null], "Passwords must match")
                                .required("You must confirm password"),
                            firstName: Yup.string()
                                .required("First name is required")
                                .max(20, "First name must be at most 20 characters"),
                            lastName: Yup.string()
                                .required("Last name is required")
                                .max(20, "Last name must be at most 20 characters"),
                            phoneNumber: Yup.string()
                                .matches(/^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s./0-9]*$/,
                                "Incorrect phone format"),
                            imagePublicId: Yup.string()
                        })}
                        onSubmit={(values, { setErrors }) => {
                            signUp({
                                email: values.email,
                                password: values.password,
                                firstName: values.firstName,
                                lastName: values.lastName,
                                phoneNumber: values.phoneNumber,
                                imagePublicId: values.imagePublicId
                            })
                            .catch(errors => setErrors(errors));
                        }}>
                        <Form>
                            <CardTitle>Credentials</CardTitle>
                            <ValidationTextField id="email" name="email" type="email" label="Email" disabled={signingUp}
                                placeholder='Enter your email'/>
                            <ValidationTextField id="password" name="password" type="password" label="Password" disabled={signingUp}
                                placeholder='Enter your password'/>
                            <ValidationTextField id="confirmPassword" name="confirmPassword" type="password" label="Confirm password" disabled={signingUp}
                                placeholder='Confirm your password'/>

                            <CardTitle>Personal data</CardTitle>
                            <ImageUpload id="imagePublicId" name="imagePublicId" disabled={signingUp}/>
                            <ValidationTextField id="firstName" name="firstName" type="text" label="First Name" disabled={signingUp}/>
                            <ValidationTextField id="lastName" name="lastName" type="text" label="Last Name" disabled={signingUp}/>
                            <ValidationTextField id="phoneNumber" name="phoneNumber" type="text" label="Phone Number" disabled={signingUp}/>

                            <LoadingButton color="primary" type="submit" isLoading={signingUp}>Sign Up</LoadingButton>
                        </Form>
                    </Formik>
                </Card>
            </Col>
        </Row>
    );
}

const connectedSignUpPage = connector(SignUpPage);
export { connectedSignUpPage as SignUpPage };