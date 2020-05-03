import React, { Component, Fragment, ReactNode } from 'react';
import { Container, Toast, ToastBody, ToastHeader } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { Router } from 'react-router-dom';
import './Layout.css';
import { connect, ConnectedProps } from 'react-redux';
import { RootState } from '../_store';
import { History } from 'history';
import * as alert from '../_store/alert/actions';
import '@fortawesome/fontawesome-free/css/all.css';

const mapState =  (state: RootState) => ({
  alerts: state.alert.alerts
});
const mapDispatch = {
  clearAlerts: () => alert.clear() 
};
const connector = connect(mapState, mapDispatch);
type PropsFromRedux = ConnectedProps<typeof connector>;
type LayoutProps = PropsFromRedux & Readonly<{ 
  children?: ReactNode;
  history: History<History.PoorMansUnknown>; 
}>;

class Layout extends Component<LayoutProps> {
  static displayName = Layout.name;

  render () {
    const alertToasts = this.props.alerts.map((alert, i) =>
      <Toast key={i} isOpen={true} onClick={() => this.props.clearAlerts()}>
        <ToastHeader 
          icon={alert.type === 'danger'?
            <i className="fas fa-exclamation-circle text-danger"></i> :
            <i className="fas fa-check-circle text-success"></i>}>
            {alert.type === 'danger' ? 
              'Error was occured' :
              'Success'}
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