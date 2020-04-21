import React, { useState } from 'react';
import './RatingInput.css';
import { useField } from 'formik';
import { Label, FormGroup } from 'reactstrap';

type RatingInputProps = Readonly<{
    id: string;
    name: string;
    label: string;
}>;

export const RatingInput = (props: RatingInputProps) => {
    const [ ,meta, helpers ] = useField(props);
    const { setValue } = helpers;
    
    const [ state, setState ] = useState({
        starsColor: [false, false, false, false, false]
    });
    const handleMouseOver = (value: number) => {
        setState({
            starsColor: state.starsColor.map((v, i, arr) => i <= value) 
        });
    };
    const handleClick = (value: number) => setValue(value + 1);
    const handleMouseLeave = () => {
        setState({
            starsColor: state.starsColor.map((v, i, arr) => i <= meta.value - 1) 
        });
    };
    const stars = [...Array(5).keys()].map(num =>
        <span 
            key={num} 
            onMouseLeave={handleMouseLeave}
            onMouseOver={() => handleMouseOver(num)} 
            onClick={() => handleClick(num)}
            className={`fa fa-star rate-star ${state.starsColor[num]? 'active-star' : ''}`}/>
    );
    return (
        <FormGroup>
            <Label htmlFor={props.id || props.name}>{props.label}</Label><br/>
            {stars}
            <span> ({meta.value})</span>
        </FormGroup>
    );
};