import React from 'react';
import { Route } from 'react-router';

import { history } from './_helpers';
import { alertActions } from './_actions';

import { Layout, PrivateRoute } from './_components';
import { LoginPage } from './LoginPage';
import { HomePage } from './HomePage';

import './custom.css'
import { SignUpPage } from './SignUpPage';
import { connect, ConnectedProps } from 'react-redux';
import { ProfilePage } from './ProfilePage';
import { EmployeePage } from './EmployeePage';
import { MakeOrderPage } from './MakeOrderPage';
import { EmployeeOrdersPage } from './EmployeeTasksPage';
import { UserOrdersPage } from './UserOrdersPage';
import { RootState } from './_store';

const mapState =  (state: RootState) => ({
  alert: state.alert
});
const connector = connect(mapState);
type PropsFromRedux = ConnectedProps<typeof connector>;
type AppProps = PropsFromRedux & {};

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');

class App extends React.Component<AppProps> {
  static displayName = App.name;

  constructor(props: AppProps) {
    super(props);

    const { dispatch } = this.props;
    history.listen((location, action) => {
      if(props.alert.message)
        dispatch(alertActions.clear());
    });
  }

  render () {
    return (
      <Layout history={history}>
        <PrivateRoute path='/orders/page/:page' component={UserOrdersPage}/>
        <PrivateRoute path='/tasks/page/:page' component={EmployeeOrdersPage}/>
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

const connectedApp = connector(App);
export { connectedApp as App };