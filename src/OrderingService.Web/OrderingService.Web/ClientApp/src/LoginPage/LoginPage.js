import React, { Component } from 'react';
import { connect } from 'react-redux';
import { Formik, Form } from 'formik';
import * as Yup from 'yup';
import { ValidationTextField, LoadingButton } from '../components';
import { Col, Card, Row, CardTitle, Alert } from 'reactstrap';
import { userActions } from '../actions';

class LoginPage extends Component {

    constructor(props){
        super(props);

        //reset login status
        this.props.dispatch(userActions.logout());
    }

    render() {
        const { alert, loggingIn } = this.props; 
        return (
            <Row className="d-flex justify-content-center mt-5">
                <Col lg={5} md={6} sm={8}>
                    <Card body>
                        <CardTitle>Login</CardTitle>
                        <Alert isOpen={!!alert.message} color={alert.type}>{alert.message}</Alert>
                        <Formik
                            initialValues={{
                                email: '',
                                password: ''
                            }}
                            validationSchema={Yup.object({
                                email: Yup.string()
                                    .email('Invalid email address')
                                    .required('Required'),
                                password: Yup.string()
                                    .required('Required')
                            })}
                            onSubmit={(values, { setSubmitting }) => {
                                this.props.dispatch(userActions.login(values.email, values.password));
                                setSubmitting(true);
                            }}>
                            <Form>
                                <ValidationTextField
                                    id="email"
                                    name="email"
                                    type="email"
                                    label="Email"
                                    disabled={loggingIn}/>
                                <ValidationTextField
                                    id="password"
                                    name="password"
                                    type="password"
                                    label="Password"
                                    disabled={loggingIn}/>
                                <LoadingButton type="submit" color="primary" isLoading={loggingIn}>Log In</LoadingButton>
                            </Form>
                        </Formik>
                    </Card>
                </Col>
            </Row>
        );
    }
}

const mapStateToProps = state => {
    const { loggingIn } = state.authentication;
    const { alert } = state;
    return {
        loggingIn,
        alert
    };
}

const connectedLoginPage = connect(mapStateToProps)(LoginPage);
export { connectedLoginPage as LoginPage };