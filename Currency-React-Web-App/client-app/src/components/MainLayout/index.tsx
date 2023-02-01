import React from 'react';
import { Container } from 'react-bootstrap';
import { NavMenu } from '../NavMenu';
import { Outlet } from 'react-router-dom';
import { useCurrencyDbUpdate } from '../../queryHooks/Currency';

export const MainLayout = () => {
  useCurrencyDbUpdate();

  return (
    <>
      <NavMenu />
      <Container>
        <Outlet />
      </Container>
    </>
  );
};
