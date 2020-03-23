import React from 'react';
import { Button } from 'reactstrap';

export const LoadingButton = ({ isLoading, ...props}) => {
    return (
        <Button {...props} disabled={isLoading}>
            {isLoading ? (<>
                <span className="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                Loading...
            </>): props.children }
        </Button>
    );
};