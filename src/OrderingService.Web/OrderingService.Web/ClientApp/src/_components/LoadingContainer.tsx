import React, { ReactNode } from 'react';
import './LoadingContainer.css';
import { Fade } from 'reactstrap';

type LoadingContanierProps = Readonly<{
    children?: ReactNode;
    isLoading: boolean;
}>;

export const LoadingContainer = (props: LoadingContanierProps) =>  {
    return (
        <div className="d-block">
        <Fade in={props.isLoading} className="position-absolute abs-center ml-n5">
            <div className="cssload-thecube">
                <div className="cssload-cube cssload-c1"></div>
                <div className="cssload-cube cssload-c2"></div>
                <div className="cssload-cube cssload-c4"></div>
                <div className="cssload-cube cssload-c3"></div>
            </div>
            <div className="d-flex justify-content-center mt-4">
                Loading...
            </div>
        </Fade>
        <Fade in={!props.isLoading}>
            {props.children}
        </Fade>
        </div>
    );
}