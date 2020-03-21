import React, { Fragment } from 'react';
import { Button } from 'reactstrap';

export const LoadingButton = ({ isLoading, ...props}) => {
    return (
        <Button {...props} disabled={isLoading}>
            {isLoading ? (
                <Fragment>
                    <span class="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                    Loading...
                </Fragment>
            ): props.children }
        </Button>
    );
};