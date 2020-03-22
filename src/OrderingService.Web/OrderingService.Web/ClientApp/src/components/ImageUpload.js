import React, { useState } from 'react';
import config from '../config';
import { useField } from 'formik';
import { FormGroup, Label } from 'reactstrap';
import './ImageUpload.css';

export const ImageUpload = ({label, ...props}) => {
    const [, setState] = useState({ isUploading: false});
    const [, meta, helpers] = useField(props);
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
            <Label htmlFor={props.id && props.name}>
                <img src={meta.value || 'images/default-user.jpg'} 
                    height="150" width="150"
                    className={'rounded '+ (props.disabled? '': 'img-upload')} 
                    alt="Profile"/>
            </Label>
            <input className="collapse" type="file" {...props}
                onChange={handleFileChange}/>
        </FormGroup>
    );
};