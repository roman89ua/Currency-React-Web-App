import { useQuery } from 'react-query';
import axios, { AxiosRequestConfig, AxiosResponse } from 'axios';
import { CurrencyFields } from '../../components/Tables/TableHeadSort/types';
import { TableOrder } from '../../components/Tables/TableHeadSort/Enums';
import { ICurrency } from '../../components/Currency';

const sortCurrencyDataRequest = ({ queryKey }: { queryKey: string[] }) => {
  const fieldName = queryKey[1];
  const order = queryKey[2];
  return axios(`currencyCurrentDate/sortCurrencyData/${fieldName}/${order}`);
};

export const useSortedCurrencyData = (
  fieldKey: CurrencyFields,
  sortOrder: TableOrder,
  onSuccess: ((data: AxiosResponse<ICurrency[], AxiosRequestConfig>) => void) | undefined,
  enabled = true,
) => {
  return useQuery(['sortCurrencyData', fieldKey, sortOrder], sortCurrencyDataRequest, {
    enabled,
    onSuccess,
  });
};
