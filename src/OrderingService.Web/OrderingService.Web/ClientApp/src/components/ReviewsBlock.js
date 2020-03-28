import React from 'react';
import { Card, CardText, CardTitle } from 'reactstrap';
import { UserWithAvatar } from './UserWithAvatar';
import { Rating } from './Rating';

export const ReviewsBlock = props => {
    return(
        <>
        <Card body className="my-2">
            <CardTitle>
                <UserWithAvatar user={props.user}/>
                <Rating className="mt-1" rate={4}/>
            </CardTitle>
            <CardText>
                Awesome! You do great job! Thank you a lot!
            </CardText>    
            <CardText><span className="text-secondary font-italic small">16:00 01/20/2020</span></CardText>
        </Card>
        <Card body className="my-2">
            <CardTitle>
                <UserWithAvatar user={props.user}/>
                <Rating className="mt-1" rate={4}/>
            </CardTitle>
            <CardText>
                Awesome! You do great job! Thank you a lot!
            </CardText>    
            <CardText><span className="text-secondary font-italic small">16:00 01/20/2020</span></CardText>
        </Card>
        </>
    );
};