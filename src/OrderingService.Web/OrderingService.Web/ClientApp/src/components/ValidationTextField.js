import React from 'react';
import { FormGroup, Label, Input, FormFeedback } from 'reactstrap';
import { useField } from 'formik';

export const ValidationTextField = ({ label, ...props}) => {
    const [field, meta] = useField(props);
    return (
        <FormGroup>
            <Label htmlFor={props.id || props.name}>{label}</Label>
            <Input { ...field } { ...props}
                valid={meta.touched && !meta.error} 
                invalid={meta.touched && !!meta.error}/>
            {meta.touched && meta.error ? (
                <FormFeedback invalid>{meta.error}</FormFeedback>
            ): null}
        </FormGroup>
    );
}