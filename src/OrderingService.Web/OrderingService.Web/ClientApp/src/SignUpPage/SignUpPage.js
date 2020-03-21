import React, { Component } from 'react';
import { Card, Row, Col, CardText, CardTitle } from 'reactstrap';
import { Field, Formik, Form, yupToFormErrors } from 'formik';
import { ImageUpload } from '../components';

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
            <Row>
                <Col>
                <img src={this.state.url} height="100" width="100"/>
                <ImageUpload imageUploaded={this.handleImageUploaded}/>
                    <Card body>
                        <CardTitle>Sign Up</CardTitle>
                        <CardText>
                        </CardText>
                    </Card>
                </Col>
            </Row>
        );
    }
}

export { SignUpPage };