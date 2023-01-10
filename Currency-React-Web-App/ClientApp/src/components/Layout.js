import React, {useEffect} from 'react';
import {Container} from 'reactstrap';
import {NavMenu} from './NavMenu';

export const Layout = ({children}) => {
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
