import React from 'react';
import { MainLayout } from './components/MainLayout';
import { Route, Routes } from 'react-router-dom';
// import AppRoutes from './AppRoutes';
import PageSpinner from './components/PageSpinner';
import { QueryClient, QueryClientProvider } from 'react-query';
import { ReactQueryDevtools } from 'react-query/devtools';

const Home = React.lazy(() => import('./pages/Home'));
const PageNotFound = React.lazy(() => import('./pages/PageNotFound'));
const Currency = React.lazy(() => import('./pages/Currency'));
const Chart = React.lazy(() => import('./pages/CurrencyChart'));

const queryClient = new QueryClient();

const App = () => (
  <QueryClientProvider client={queryClient}>
    <React.Suspense fallback={<PageSpinner />}>
      <Routes>
        <Route element={<MainLayout />} path="/">
          {/* {AppRoutes.map((route) => { */}
          {/*   const { element, ...rest } = route; */}
          {/*   return <Route key={crypto.randomUUID()} {...rest} element={element} />; */}
          {/* })} */}
          <Route index element={<Home />} />
          <Route element={<Currency />} path="/Currency" />
          <Route element={<Chart />} path="/Currency/:currencyCode" />
          <Route element={<PageNotFound />} path="*" />
        </Route>
      </Routes>
    </React.Suspense>
    <ReactQueryDevtools initialIsOpen={false} position="bottom-right" />
  </QueryClientProvider>
);

export default App;
