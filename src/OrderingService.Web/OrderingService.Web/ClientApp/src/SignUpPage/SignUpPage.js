import React, { Component } from 'react';
import { Card, Row, Col, CardTitle } from 'reactstrap';
import { Formik, Form } from 'formik';
import * as Yup from 'yup';
import { ValidationTextField, LoadingButton, ImageUpload } from '../components';

class SignUpPage extends Component{
    constructor(props){
        super(props);

        this.state = {
            url: ''
        }

        this.handleImageUploaded = this.handleImageUploaded.bind(this);
    }

    handleImageUploaded(url){
        this.setState({
            url
        });
    }

    render(){
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
                                imageUrl: null
                            }}
                            validationSchema={Yup.object({
                                email: Yup.string()
                                    .required("Email is required")
                                    .email("Incorrect email"),
                                password: Yup.string()
                                    .required("Password is required")
                                    .min(8, "Password length must contain at least 8 symbols"),
                                confirmPassword: Yup.string()
                                    .oneOf([Yup.ref('password'), null], "Passwords must match")
                                    .required("You must confirm password"),
                                firstName: Yup.string()
                                    .required("First name is required")
                                    .max(20, "First name must be at most 20 characters"),
                                lastName: Yup.string()
                                    .required("Last name is required")
                                    .max(20, "Last name must be at most 20 characters"),
                                phoneNumber: Yup.string(),
                                imageUrl: Yup.string().url()
                            })}>
                            <Form>
                                <CardTitle>Credentials</CardTitle>
                                <ValidationTextField id="email" name="email" type="email" label="Email"/>
                                <ValidationTextField id="password" name="password" type="password" label="Password"/>
                                <ValidationTextField id="confirmPassword" name="confirmPassword" type="password" label="Confirm password"/>

                                <CardTitle>Personal data</CardTitle>
                                <ImageUpload label="Profile image" accepts="image/jpg" id="imageUrl" name="imageUrl" />
                                <ValidationTextField id="firstName" name="firstName" type="text" label="First Name"/>
                                <ValidationTextField id="lastName" name="lastName" type="text" label="Last Name"/>
                                <ValidationTextField id="phoneNumber" name="phoneNumber" type="text" label="Phone Number"/>

                                <LoadingButton color="primary" type="submit">Sign Up</LoadingButton>
                            </Form>
                        </Formik>
                    </Card>
                </Col>
            </Row>
        );
    }
}

export { SignUpPage };