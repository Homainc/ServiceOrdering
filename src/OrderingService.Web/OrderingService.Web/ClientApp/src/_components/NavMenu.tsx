import React, { Component } from 'react';
import { Collapse, Container, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, UncontrolledDropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';
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
                  {user?.role === 'admin' && (
                    <UncontrolledDropdown nav inNavbar>
                      <DropdownToggle className="text-light" nav caret>Manage</DropdownToggle>
                      <DropdownMenu>
                        <DropdownItem>
                          <Link className='text-dark text-decoration-none' to='/service-types'>Service Types</Link>
                        </DropdownItem>
                      </DropdownMenu>
                    </UncontrolledDropdown>
                  )}
                  <UncontrolledDropdown nav inNavbar>
                      <DropdownToggle className="text-light" nav caret>
                        <UserWithAvatar user={user}/>
                      </DropdownToggle>
                      <DropdownMenu>
                        <DropdownItem>
                          <Link className="text-dark text-decoration-none" to="/profile">Profile</Link>
                        </DropdownItem>
                        <DropdownItem>
                          <Link className="text-dark text-decoration-none" to="/orders/page/1">My orders</Link>
                        </DropdownItem>
                        <DropdownItem>
                          {user && user.employeeProfile && (
                            <Link className="text-dark text-decoration-none" to="/tasks/page/1">My Tasks</Link>
                          )}
                        </DropdownItem>
                      </DropdownMenu>
                    </UncontrolledDropdown>
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
