import React from 'react';
import '@fortawesome/fontawesome-free/css/all.css';

export const Rating = ({rate, ...props}) => {
    const value = rate.toFixed() - 1;
    const stars = [...Array(5).keys()].map(num =>
        <span key={num} className={`fa fa-star ${num > value? 'text-secondary' : 'text-primary'}`}/>
    );
    return(
        <div {...props}>
        {stars}
        <span className="text-secondary"> ({rate})</span>
        </div>
    );
};