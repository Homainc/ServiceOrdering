import React from 'react';
import { useField } from 'formik';
import DatePicker from 'react-datepicker';
import "react-datepicker/dist/react-datepicker.css";
import { Label, FormGroup } from 'reactstrap';


export const FormikDatePicker = ({ label, ...props }) => {
    const [, meta, helpers] = useField(props);
    const { setValue } = helpers;
    return (
        <FormGroup>
            <Label 
                htmlFor={props.id || props.name}>
                    {label}
            </Label><br/>
            <DatePicker
                {...props} 
                selected={meta.value} 
                onChange={date => setValue(date)} 
                showTimeSelect 
                className="form-control"
                timeIntervals={15}
                dateFormat="MMMM d, yyyy h:mm aa"/>
        </FormGroup>
    );
};