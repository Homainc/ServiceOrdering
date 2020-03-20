import React, { Component, Fragment } from 'react';
import { Container } from 'reactstrap';
import { NavMenu } from './NavMenu';
import { BrowserRouter } from 'react-router-dom';

export class Layout extends Component {
  static displayName = Layout.name;

  render () {
    return (
      <div>
        <BrowserRouter basename={this.props.basename} history={this.props.history}>
          <Fragment>
            <NavMenu />
            
            <Container>
              {this.props.children}
            </Container>
          </Fragment>
        </BrowserRouter>
      </div>
    );
  }
}
