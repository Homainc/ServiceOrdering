import React from 'react';
import { connect, ConnectedProps } from 'react-redux';
import { Formik, Form } from 'formik';
import * as Yup from 'yup';
import { ValidationTextField, LoadingButton } from '../_components';
import { Col, Card, Row, CardTitle, Alert } from 'reactstrap';
import { RootState } from '../_store';
import { AuthState, AuthActionTypes } from '../_store/auth/types';
import { ThunkDispatch } from 'redux-thunk';
import * as authActions from '../_store/auth/actions';

const mapState = (state: RootState) => ({
    loggingIn: state.auth.loggingIn,
    loggedIn: state.auth.loggedIn,
    alert: state.alert
});

const mapDispatch = (
    dispatch: ThunkDispatch<AuthState, undefined, AuthActionTypes>
) => ({
    logOut: () => dispatch(authActions.logOut()),
    logIn: (username: string, password: string) => dispatch(authActions.logIn(username, password))
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type LoginPageProps = PropsFromRedux & Readonly<{}>;

class LoginPage extends React.Component<LoginPageProps> {

    constructor(props: LoginPageProps){
        super(props);

        //reset login status
        if(props.loggedIn)
            props.logOut();
    }

    render() {
        const { alert, loggingIn, logIn } = this.props; 
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
                                logIn(values.email, values.password);
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

const connectedLoginPage = connector(LoginPage);
export { connectedLoginPage as LoginPage };