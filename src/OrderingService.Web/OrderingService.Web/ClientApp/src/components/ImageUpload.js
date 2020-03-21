import React, { useState } from 'react';
import config from '../config';
import { useField } from 'formik';
import { FormGroup, Label, Row, Col } from 'reactstrap';
import './ImageUpload.css';

export const ImageUpload = ({label, imageUploaded, ...props}) => {
    const [state, setState] = useState({ isUploading: false});
    const [field, meta, helpers] = useField(props);
    const { setValue } = helpers;

    const handleFileChange = ({ target }) => {
        setState({ isUploading: true });
        const data = new FormData();
        data.append('upload_preset', config.cloudinaryUploadPreset);
        data.append('file', target.files[0]);

        fetch(config.cloudinaryApiUrl, {
            method: "POST",
            body: data
        }).then(resp => {
            return resp.json().then(data => {
                if(resp.ok){
                    const url = data['secure_url'];
                    setValue(url);
                    setState({ isUploading: false });
                }
            });
        });
    }

    return (
        <FormGroup>
            <Label htmlFor={props.id && props.name}>{label}</Label><br/>
            <Row>
                <Col md="2">
                    <img src={meta.value || 'images/default-user.jpg'} height="100" width="100"/>
                </Col>
                <Col className="d-flex align-items-center">
                    <input type="file" {...props} 
                        onChange={handleFileChange}
                        disabled={state.isUploading}/>
                </Col>
            </Row>
        </FormGroup>
    );
};