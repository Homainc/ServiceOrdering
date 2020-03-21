import React from 'react';
import { Route } from 'react-router';

import { history } from './helpers';
import { alertActions } from './actions';

import { Layout } from './components';
import { LoginPage } from './LoginPage';
import { HomePage } from './HomePage';

import './custom.css'
import { SignUpPage } from './SignUpPage';
import { connect } from 'react-redux';

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
    return (
      <Layout basename={baseUrl} history={history}>
        <Route path='/signup' component={SignUpPage} />
        <Route path='/login' component={LoginPage}/>
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