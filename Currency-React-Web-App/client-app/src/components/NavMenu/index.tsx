import React from 'react';
import { Container, Nav, Navbar } from 'react-bootstrap';
import AppRoutes from '../../AppRoutes';
import { Link } from 'react-router-dom';

export const NavMenu = () => (
  <header>
    <Navbar bg="light" expand="lg">
      <Container>
        <Navbar.Brand>
          <Link
            className="text-decoration-none text-black"
            to={AppRoutes.find((route) => route.title === 'Home')?.path || ''}
          >
            Currency-React-Web-App
          </Link>
        </Navbar.Brand>
        <Navbar.Toggle aria-controls="main-navbar" />
        <Navbar.Collapse id="main-navbar">
          <Nav className="ms-auto">
            {AppRoutes.map((route) => {
              if (route.path === '/' || route.path === '/Currency') {
                return (
                  <Link
                    key={route.path}
                    role="button"
                    className="ms-auto nav-link text-decoration-none text-black-50"
                    to={route?.path || ''}
                  >
                    {route.title}
                  </Link>
                );
              }
            })}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  </header>
);
