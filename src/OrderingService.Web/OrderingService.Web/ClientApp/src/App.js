import React from 'react';
import { Route } from 'react-router';

import { history } from './helpers';
import { alertActions } from './actions';

import { Layout, PrivateRoute } from './components';
import { LoginPage } from './LoginPage';
import { HomePage } from './HomePage';

import './custom.css'
import { SignUpPage } from './SignUpPage';
import { connect } from 'react-redux';
import { ProfilePage } from './ProfilePage';
import { EmployeePage } from './EmployeePage';
import { MakeOrderPage } from './MakeOrderPage';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');

class App extends React.Component {
  static displayName = App.name;

  constructor(props) {
    super(props);

    const { dispatch } = this.props;
    history.listen((location, action) => {
      if(props.alert.message)
        dispatch(alertActions.clear());
    });
  }

  render () {
    return (
      <Layout basename={baseUrl} history={history}>
        <PrivateRoute path='/order/:employeeId' component={MakeOrderPage}/>
        <Route path='/employee/:id' component={EmployeePage}/>
        <Route path='/page/:page' component={HomePage}/>
        <PrivateRoute path='/profile' component={ProfilePage} />
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