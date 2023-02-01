import React from 'react';
import { MainLayout } from './components/MainLayout';
import { Route, Routes } from 'react-router-dom';
// import AppRoutes from './AppRoutes';
import PageSpinner from './components/PageSpinner';
import { QueryClient, QueryClientProvider } from 'react-query';
import { ReactQueryDevtools } from 'react-query/devtools';
import Home from './pages/Home';
import PageNotFound from './pages/PageNotFound';
import Currency from './pages/Currency';
import Chart from './pages/Chart';

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
          <Route index element={<Home />}></Route>
          <Route element={<Chart />} path="currency/:currencyCode" />
          <Route element={<Currency />} path="currency"></Route>
          <Route element={<PageNotFound />} path="*" />
        </Route>
      </Routes>
    </React.Suspense>
    <ReactQueryDevtools initialIsOpen={false} position="bottom-right" />
  </QueryClientProvider>
);

export default App;
