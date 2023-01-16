import axios, { AxiosRequestConfig, AxiosResponse } from 'axios';
import { useQuery } from 'react-query';
import { ICurrency } from '../../components/Currency';

const getCurrencyData = async () => await axios.get('currencyCurrentDate');

export const useCurrentDataCurrency = (
  onSuccess?: ((data: AxiosResponse<ICurrency[], AxiosRequestConfig>) => void) | undefined,
  refetchOnWindowFocus = true,
) => {
  return useQuery('currencyCurrentDate', getCurrencyData, {
    onSuccess,
    refetchOnWindowFocus,
  });
};
