import { useQuery } from 'react-query';
import { api } from '../../api';

const getSingleCurrencyData = async ({ queryKey }: { queryKey: any[] }) => {
  const startDate = queryKey[1];
  const endDate = queryKey[2];
  const currencyCode = queryKey[3];

  return await api('/currency/SingleCurrencyByDates', {
    method: 'GET',
    params: {
      startDate,
      endDate,
      currencyCode,
    },
  });
};

export const useCurrencyRateHistoryData = (
  startDate: string | null,
  endDate: string | null,
  currencyCode: string | null,
  enabled: boolean,
) =>
  useQuery(['SingleCurrencyDataByDates', startDate, endDate, currencyCode], getSingleCurrencyData, {
    enabled,
  });
