import React from 'react';
import '@fortawesome/fontawesome-free/css/all.css';

export const Rating = props => {
    const value = props.rate.toFixed() - 1;
    const stars = [...Array(5).keys()].map(num =>
        <span key={num} className={`fa fa-star ${num > value? 'text-secondary' : 'text-primary'}`}/>
    );
    return(
        <>
        {stars}
        <span className="text-secondary"> ({props.reviews > 0 ? props.reviews : 'no'} reviews)</span>
        </>
    );
};