import React from 'react';
import { Route } from 'react-router';
import { connect } from 'react-redux';

import { history } from './helpers';
import { alertActions } from './actions';

import { PrivateRoute, Layout } from './components';
import { BrowserRouter } from 'react-router-dom';
import { HomePage } from './HomePage';

import './custom.css'

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');

class App extends React.Component {
  static displayName = App.name;

  constructor(props) {
    super(props);

    const { dispatch } = this.props;
    history.listen((location, action) => {
      dispatch(alertActions.clear());
    });
  }

  render () {
    const { alert } = this.props;
    return (
      <Layout basename={baseUrl} history={history}>
        <Route exact path='/' component={HomePage} />
      </Layout>
    );
  }
}

function mapStateToProps(state){
  const { alert } = state;
  return {
    alert
  };
}

const connectedApp = connect(mapStateToProps)(App);
export { connectedApp as App };