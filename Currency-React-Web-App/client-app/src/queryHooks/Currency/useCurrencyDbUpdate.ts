import { useQuery } from 'react-query';
import { api } from '../../api';

export const useCurrencyDbUpdate = () =>
  useQuery('updateDbOnAppStart', () => api.get('/home/updateDbOnAppStart'), {
    staleTime: 60000,
    refetchOnMount: false,
  });
