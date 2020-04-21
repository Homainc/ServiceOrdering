import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';
import { connect, ConnectedProps } from 'react-redux';
import { UserWithAvatar } from './UserWithAvatar';
import { RootState } from '../_store';

const mapState =  (state: RootState) => ({
  auth: state.auth
});
const connector = connect(mapState);
type PropsFromRedux = ConnectedProps<typeof connector>;
type NavMenuProps = PropsFromRedux & {};
type NavMenuState = {
  collapsed: boolean;
};

class NavMenu extends Component<NavMenuProps, NavMenuState> {
  static displayName = NavMenu.name;

  constructor(props: NavMenuProps) {
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
    const { loggedIn, user } = this.props.auth;
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
                  <NavItem>
                    <NavLink tag={Link} className="text-light" to="/orders/page/1">My orders</NavLink>
                  </NavItem>
                  {user && user.employeeProfile && (
                    <NavItem>
                      <NavLink tag={Link} className="text-light" to="/tasks/page/1">Tasks</NavLink>
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

const connectedNavMenu = connector(NavMenu);
export { connectedNavMenu as NavMenu };