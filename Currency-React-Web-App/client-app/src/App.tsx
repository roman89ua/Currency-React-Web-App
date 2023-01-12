import React from 'react';
import { MainLayout } from './components/MainLayout';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';

const App = () => (
  <MainLayout>
    <Routes>
      {AppRoutes.map((route) => {
        const { element, ...rest } = route;
        return <Route key={crypto.randomUUID()} {...rest} element={element()} />;
      })}
    </Routes>
  </MainLayout>
);

export default App;
