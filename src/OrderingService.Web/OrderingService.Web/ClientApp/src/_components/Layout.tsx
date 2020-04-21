import React, { Component, Fragment, ReactNode } from 'react';
import { Container, Toast, ToastBody, ToastHeader } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { Router } from 'react-router-dom';
import './Layout.css';
import { connect, ConnectedProps } from 'react-redux';
import { RootState } from '../_store';
import { History } from 'history';
import * as alert from '../_store/alert/actions';

const mapState =  (state: RootState) => ({
  alert: state.alert
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
            <Toast className="toast-pos" isOpen={this.props.alert && this.props.alert.type === 'success'} onClick={() => this.props.clearAlerts()}>
              <ToastHeader>
                Notice
              </ToastHeader>
              <ToastBody>
                {this.props.alert && this.props.alert.message}
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