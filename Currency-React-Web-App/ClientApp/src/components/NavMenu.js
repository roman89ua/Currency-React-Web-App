import React, {useState} from 'react';
import {Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink} from 'reactstrap';
import {Link} from 'react-router-dom';
import './NavMenu.css';

export const NavMenu = () => {
  const [collapsed, setCollapsed] = useState(true);

  const toggleNavbar = () => {
    setCollapsed((prevState) => !prevState);
  }

  return (
    <header>
      <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light>
        <NavbarBrand tag={Link} to="/">Currency_React_Web_App</NavbarBrand>
        <NavbarToggler onClick={toggleNavbar} className="mr-2"/>
        <Collapse className="d-sm-inline-flex flex-sm-row-reverse" isOpen={!collapsed} navbar>
          <ul className="navbar-nav flex-grow">
            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/">Home</NavLink>
            </NavItem>

            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/currency">Currency</NavLink>
            </NavItem>
          </ul>
        </Collapse>
      </Navbar>
    </header>
  );
}


