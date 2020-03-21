import React, { Component } from 'react';
import config from '../config';
import { LoadingButton } from './LoadingButton';

export class ImageUpload extends Component {

    constructor(props){
        super(props);

        this.state = {
            isUploading: false
        }
        this.AJAXSubmit = this.AJAXSubmit.bind(this);
    }

    AJAXSubmit(e) {
        e.preventDefault();
        this.setState({
            isUploading: true
        });

        const form = e.target;
        if (!form.action) { return; }
        const xhr = new XMLHttpRequest();
        xhr.onload = e => {
            const resp = JSON.parse(xhr.responseText);
            this.props.imageUploaded(resp['secure_url']);
            this.setState({
                isUploading: false
            });
        };
        xhr.open("post", config.cloudinaryApiUrl);
        let formData = new FormData(form);
        formData.append('upload_preset', config.cloudinaryUploadPreset);
        xhr.send(formData);
    }

    render(){

    
    return(
        <>
        <form onSubmit={this.AJAXSubmit} method="post" encType="multipart/form-data">
            <input type="file" name="file" accept="image/jpeg"/>
            <LoadingButton type="submit" isLoading={this.state.isUploading}>Load</LoadingButton>
        </form>
        </>
    );
    }
};