import React from 'react';
import { Route, Redirect, RouteProps } from 'react-router-dom';

type PrivateRouteProps = Readonly<{
    component: React.ComponentType<any>;
}> & RouteProps;

export const PrivateRoute = ({ component: Component, ...rest}: PrivateRouteProps) => (
    <Route {...rest} render={props => (
        localStorage.getItem('user')
            ? <Component {...props} />
            : <Redirect to={{ pathname: '/login', state: {from: props.location}}} />
    )}/>
);