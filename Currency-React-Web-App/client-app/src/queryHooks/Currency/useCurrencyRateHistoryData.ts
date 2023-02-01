import { useQuery, useQueryClient } from 'react-query';
import { api } from '../../api';

const getSingleCurrencyData = async ({ queryKey }: { queryKey: any[] }) => {
  const params = queryKey[1];

  return await api('/SingleCurrency', {
    method: 'GET',
    params,
  });
};
const a = () => {
  console.log('request');
};
export const useCurrencyRateHistoryData = (params: { [key: string]: string }) => {
  const queryClient = useQueryClient();
  return useQuery(['SingleCurrencyDataByDates', params], a, {
    initialData: () => {
      const queryData = queryClient?.getQueryData('currencyCurrentDate') || {};
      console.log({ queryData });
      return undefined;
    },
  });
};
