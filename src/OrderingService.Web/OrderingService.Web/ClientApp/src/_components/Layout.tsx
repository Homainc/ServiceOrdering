import React, { Component, Fragment, ReactNode } from 'react';
import { Container, Toast, ToastBody, ToastHeader } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { Router } from 'react-router-dom';
import './Layout.css';
import { connect, ConnectedProps } from 'react-redux';
import { RootState } from '../_store';
import { History } from 'history';
import * as alertActions from '../_store/alert/actions';
import { HubConnectionBuilder, HubConnection } from '@aspnet/signalr';
import '@fortawesome/fontawesome-free/css/all.css';
import { ThunkDispatch } from 'redux-thunk';
import { AlertActionTypes } from '../_store/alert/types';

const mapState =  (state: RootState) => ({
  alerts: state.alert.alerts,
  loggedIn: state.auth.loggedIn,
  userToken: state.auth.user?.token
});

const mapDispatch = (
  dispatch: ThunkDispatch<RootState, undefined, AlertActionTypes>
) => ({
  pushInfoAlert: (msg: string) => dispatch(alertActions.info(msg)),
  clearAlerts: () => alertActions.clear()
});

const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type LayoutProps = PropsFromRedux & Readonly<{ 
  children?: ReactNode;
  history: History<History.PoorMansUnknown>; 
}>;

type LayoutState = {
  notificationHub?: HubConnection;
};

class Layout extends Component<LayoutProps, LayoutState> {
  static displayName = Layout.name;

  constructor(props: LayoutProps){
    super(props);

    this.state = {
      notificationHub: undefined
    };
  }

  componentDidMount = () => {
    if(this.props.loggedIn){
      const hubConnection = new HubConnectionBuilder()
        .withUrl('/notification', { accessTokenFactory: () => this.props.userToken || '' })
        .build();
      
      this.setState({ notificationHub: hubConnection }, () => {
        this.state.notificationHub
          ?.start()
          .catch((err: any) => console.log(`Error while establishing connection: ${err}`));

        this.state.notificationHub?.on('ReceiveNotice', msg => this.props.pushInfoAlert(msg));
      });
    }
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