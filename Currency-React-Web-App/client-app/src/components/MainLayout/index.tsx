import React, {ReactNode, useEffect} from 'react';
import {Container} from 'react-bootstrap';
import { NavMenu } from '../NavMenu';

export const MainLayout = ({children}: {children: ReactNode}) => {
  const onHomeTrigger = async () => await fetch('home/updateDbOnAppStart')
  
  useEffect(() => {
    onHomeTrigger();
  }, []);
  
  return (
    <div>
      <NavMenu/>
      <Container>
        {children}
      </Container>
    </div>
  )
}
