import React from 'react';
import { Route } from 'react-router';

import { history } from './helpers';
import { alertActions } from './actions';

import { Layout } from './components';
import { LoginPage } from './LoginPage';
import { HomePage } from './HomePage';

import './custom.css'

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');

export class App extends React.Component {
  static displayName = App.name;

  constructor(props) {
    super(props);

    const { dispatch } = this.props;
    history.listen((location, action) => {
      dispatch(alertActions.clear());
    });
  }

  render () {
    return (
      <Layout basename={baseUrl} history={history}>
        <Route path='/login' component={LoginPage}/>
        <Route exact path='/' component={HomePage} />
      </Layout>
    );
  }
}