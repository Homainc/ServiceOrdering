import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { connect } from 'react-redux';
import { UserWithAvatar } from './UserWithAvatar';

class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    const { loggedIn, user } = this.props.authentication;
    return (
      <header>
        <Navbar color="dark" className="navbar-expand-sm navbar-toggleable-sm ng-dark border-bottom box-shadow mb-3" dark>
          <Container>
            <NavbarBrand tag={Link} to="/">Ordering Service</NavbarBrand>
            <NavbarToggler onClick={this.toggleNavbar} className="mr-2" />
            <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!this.state.collapsed} navbar>
              <ul className="navbar-nav flex-grow">
                <NavItem>
                  <NavLink tag={Link} className="text-light" to="/">Home</NavLink>
                </NavItem>
                { loggedIn? (
                  <>
                  <NavItem>
                     <NavLink tag={Link} className="text-light" to="/profile"><UserWithAvatar user={user}/></NavLink>
                   </NavItem>
                  {user.employeeProfile && (
                    <NavItem>
                      <NavLink tag={Link} className="text-light" to="/orders">Orders</NavLink>
                    </NavItem>
                  )}
                  </>
                ):(
                  <NavItem>
                    <NavLink tag={Link} className="text-light" to="/signup">Sign Up</NavLink>
                  </NavItem>
                ) } 
                <NavItem>
                  <NavLink tag={Link} className="text-light" to="/login">{loggedIn? 'Logout' : 'Login'}</NavLink>
                </NavItem>
              </ul>
            </Collapse>
          </Container>
        </Navbar>
      </header>
    );
  }
}

function mapStateToProps(state){
  const { authentication } = state;
  return {
    authentication
  };
}

const connectedNavMenu = connect(mapStateToProps)(NavMenu);
export { connectedNavMenu as NavMenu };
