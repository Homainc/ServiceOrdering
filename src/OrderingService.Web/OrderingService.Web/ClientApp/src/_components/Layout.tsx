import React, { Component, Fragment, ReactNode } from 'react';
import { Container, Toast, ToastBody, ToastHeader } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { Router } from 'react-router-dom';
import './Layout.css';
import { connect, ConnectedProps } from 'react-redux';
import { RootState } from '../_store';
import { History } from 'history';
import * as alertActions from '../_store/alert/actions';
import * as authActions from '../_store/auth/actions';
import '@fortawesome/fontawesome-free/css/all.css';
import { ThunkDispatch } from 'redux-thunk';
import { AlertActionTypes } from '../_store/alert/types';
import { AuthActionTypes } from '../_store/auth/types';

const mapState =  (state: RootState) => ({
  alerts: state.alert.alerts,
  loggedIn: state.auth.loggedIn
});

const mapDispatch = (
  dispatch: ThunkDispatch<RootState, undefined, AlertActionTypes | AuthActionTypes>
) => ({
  connectToNotificationHub: () => dispatch(authActions.connectToNotificationHub()),
  clearAlerts: () => alertActions.clear()
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type LayoutProps = PropsFromRedux & Readonly<{ 
  children?: ReactNode;
  history: History<History.PoorMansUnknown>; 
}>;

class Layout extends Component<LayoutProps> {
  static displayName = Layout.name;

  componentDidMount = () => {
    if(this.props.loggedIn)
      this.props.connectToNotificationHub();
  };

  getAlertClassName = (alertType: string) => {
    switch(alertType){
      case 'success':
        return 'fas fa-check-circle text-success';
      case 'danger':
        return 'fas fa-exclamation-circle text-danger';
      default:
        return 'fas fa-bell text-primary';
    }
  };

  render () {
    const alertToasts = this.props.alerts.map((alert, i) =>
      <Toast key={i} isOpen={true} onClick={() => this.props.clearAlerts()}>
        <ToastHeader 
          icon={<i className={this.getAlertClassName(alert.type)}></i>}>
            {alert.title}
        </ToastHeader>
        <ToastBody>
          {alert.message}
        </ToastBody>
      </Toast>
    );
    return (
      <div>
        <Router history={this.props.history}>
          <Fragment>
            <NavMenu />

            <Container>
              {this.props.children}
            </Container>
            <div className='toast-pos'>
              {alertToasts}
            </div>
          </Fragment>
        </Router>
      </div>
    );
  }
}

const connectedLayout = connector(Layout);
export { connectedLayout as Layout };