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
  alertType: state.alert.type,
  alertMessage: state.alert.message
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
    return (
      <div>
        <Router history={this.props.history}>
          <Fragment>
            <NavMenu />

            <Container>
              {this.props.children}
            </Container>
            <Toast className="toast-pos" isOpen={!!this.props.alertMessage} onClick={() => this.props.clearAlerts()}>
              <ToastHeader 
              icon={this.props.alertType === 'danger'?
                <i className="fas fa-exclamation-circle text-danger"></i> :
                <i className="fas fa-check-circle text-success"></i>}>
                {this.props.alertType === 'danger' ? 
                  'Error was occured' :
                  'Success'}
              </ToastHeader>
              <ToastBody>
                {this.props.alertMessage}
              </ToastBody>
            </Toast>
          </Fragment>
        </Router>
      </div>
    );
  }
}

const connectedLayout = connector(Layout);
export { connectedLayout as Layout };