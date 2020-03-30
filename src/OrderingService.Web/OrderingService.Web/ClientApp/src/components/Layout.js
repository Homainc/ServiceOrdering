import React, { Component, Fragment } from 'react';
import { Container, Toast, ToastBody, ToastHeader } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { Router } from 'react-router-dom';
import './Layout.css';
import { alertActions } from '../actions';
import { connect } from 'react-redux';

class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <Router basename={this.props.basename} history={this.props.history}>
          <Fragment>
            <NavMenu />

            <Container>
              {this.props.children}
            </Container>
            <Toast className="toast-pos" isOpen={this.props.alert && this.props.alert.type == 'success'} onClick={() => this.props.clearAlerts()}>
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

const mapStateToProps = state => {
  return {
    alert: state.alert
  };
};

const mapDispatchToProps = dispatch => ({
  clearAlerts: () => dispatch(alertActions.clear())
});

const connectedLayout = connect(mapStateToProps, mapDispatchToProps)(Layout);
export { connectedLayout as Layout };