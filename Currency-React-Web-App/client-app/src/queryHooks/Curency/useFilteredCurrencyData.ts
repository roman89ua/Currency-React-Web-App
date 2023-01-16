import { useQuery } from 'react-query';
import axios, { AxiosRequestConfig, AxiosResponse } from 'axios';
import { ICurrency } from '../../components/Currency';

const getFilteredCurrencyData = async ({ queryKey }: { queryKey: string[] }) => {
  const searchParam = queryKey[1];
  return await axios.get(`currencyCurrentDate/filterCurrencyData/${searchParam}`);
};
export const useFilteredCurrencyData = (
  enabled: boolean,
  filterValue: string,
  onSuccess: ((data: AxiosResponse<ICurrency[], AxiosRequestConfig>) => void) | undefined,
) => {
  return useQuery(['filterCurrencyData', filterValue], getFilteredCurrencyData, {
    enabled,
    onSuccess,
  });
};
