import { useQuery } from 'react-query';
import axios from 'axios';
import { CurrencyFields } from '../../components/Tables/TableHeadSort/types';
import { TableOrder } from '../../components/Tables/TableHeadSort/Enums';
import { DEFAULT_SEARCH_CURRENCY_VALUE } from '../../pages/Currency';

const getFilteredAndSortedCurrencyData = async ({ queryKey }: { queryKey: string[] }) => {
  const searchValue = queryKey[1];
  const key = queryKey[2];
  const order = queryKey[3];
  return await axios.get('currencyCurrentDate', {
    method: 'GET',
    params: {
      searchValue,
      key,
      order,
    },
  });
};
export const useSearchCurrencyData = (
  queryName: string,
  enabled: boolean,
  fieldKey: CurrencyFields,
  sortOrder: TableOrder,
  searchValue = DEFAULT_SEARCH_CURRENCY_VALUE,
) => {
  return useQuery([queryName, searchValue, fieldKey, sortOrder], getFilteredAndSortedCurrencyData, {
    enabled,
  });
};
