import { useQuery } from 'react-query';
import { api } from '../../api';

const getSingleCurrencyData = async ({ queryKey }: { queryKey: any[] }) => {
  const params = queryKey[1];

  return await api('/SingleCurrency', {
    method: 'GET',
    params,
  });
};

export const useCurrencyRateHistoryData = (params: { [key: string]: string }) =>
  useQuery(['SingleCurrencyDataByDates', params], getSingleCurrencyData);
