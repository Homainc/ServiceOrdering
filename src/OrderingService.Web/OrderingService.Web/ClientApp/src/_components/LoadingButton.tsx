import React, { ReactNode } from 'react';
import { Button, ButtonProps } from 'reactstrap';

type LoadingButtonProps = Readonly<{
    isLoading: boolean;
    children?: ReactNode;
}> & ButtonProps;

export const LoadingButton = ({ isLoading, ...props}: LoadingButtonProps ) => {
    return (
        <Button {...props} disabled={isLoading}>
            {isLoading ? (<>
                <span className="spinner-grow spinner-grow-sm" role="status" aria-hidden="true"></span>
                Loading...
            </>): props.children }
        </Button>
    );
};