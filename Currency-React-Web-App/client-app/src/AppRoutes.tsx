import React from 'react';
import { RouteProps } from 'react-router/dist/lib/components';

type AppRoutesType = RouteProps & { title: string };

const Home = React.lazy(() => import('./pages/Home'));
const Currency = React.lazy(() => import('./pages/Currency'));

const AppRoutes: AppRoutesType[] = [
  {
    index: true,
    title: 'Home',
    element: <Home />,
    path: '/',
  },
  {
    title: 'Currency',
    element: <Currency />,
    path: '/currency',
  },
];

export default AppRoutes;
