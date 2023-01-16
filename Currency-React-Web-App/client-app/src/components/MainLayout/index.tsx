import React, { ReactNode } from 'react';
import { Container } from 'react-bootstrap';
import { NavMenu } from '../NavMenu';
import axios from 'axios';
import { useQuery } from 'react-query';

export const MainLayout = ({ children }: { children: ReactNode }) => {
  useQuery('home/updateDbOnAppStart', () => axios.get('home/updateDbOnAppStart'), {
    staleTime: 60000,
    refetchOnMount: false,
  });

  return (
    <div>
      <NavMenu />
      <Container>{children}</Container>
    </div>
  );
};
