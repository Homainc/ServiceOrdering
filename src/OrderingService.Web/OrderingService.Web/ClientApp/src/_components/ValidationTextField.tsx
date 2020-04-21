import React from 'react';
import { FormGroup, Label, Input, FormFeedback, InputProps } from 'reactstrap';
import { useField } from 'formik';

type ValidationTextFieldProps = Readonly<{
    label: string;
}> & InputProps;

export const ValidationTextField = ({ label, ...props}: ValidationTextFieldProps) => {
    const [field, meta] = useField(props as { name: string });
    return (
        <FormGroup>
            <Label htmlFor={props.id || props.name}>{label}</Label>
            <Input { ...field } { ...props}
                valid={meta.touched && !meta.error} 
                invalid={meta.touched && !!meta.error}/>
            {meta.touched && meta.error ? (
                <FormFeedback invalid="true">{meta.error}</FormFeedback>
            ): null}
        </FormGroup>
    );
}