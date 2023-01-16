import React from 'react';
import { MainLayout } from './components/MainLayout';
import { Route, Routes } from 'react-router-dom';
import AppRoutes from './AppRoutes';
import PageSpinner from './components/PageSpinner';
import { QueryClient, QueryClientProvider } from 'react-query';
import { ReactQueryDevtools } from 'react-query/devtools';

const queryClient = new QueryClient();

const App = () => (
  <QueryClientProvider client={queryClient}>
    <MainLayout>
      <React.Suspense fallback={<PageSpinner />}>
        <Routes>
          {AppRoutes.map((route) => {
            const { element, ...rest } = route;
            return <Route key={crypto.randomUUID()} {...rest} element={element} />;
          })}
        </Routes>
      </React.Suspense>
    </MainLayout>
    <ReactQueryDevtools initialIsOpen={false} position="bottom-right" />
  </QueryClientProvider>
);

export default App;
