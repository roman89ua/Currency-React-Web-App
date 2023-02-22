import { useQuery } from 'react-query';
import { CurrencyFields } from '../../components/Tables/TableHeadSort/types';
import { TableOrder } from '../../components/Tables/TableHeadSort/Enums';
import { DEFAULT_SEARCH_CURRENCY_VALUE } from '../../components/Currency/constants';
import { api } from '../../api';

const getFilteredAndSortedCurrencyData = async ({ queryKey }: { queryKey: string[] }) => {
  /*
  "null" is the value which is excepted by BE === means no filter value
*/
  const searchValue = queryKey[1] || DEFAULT_SEARCH_CURRENCY_VALUE;
  const key = queryKey[2];
  const order = queryKey[3];
  return await api('currency', {
    method: 'GET',
    params: {
      searchValue,
      key,
      order,
    },
  });
};

export const useSearchCurrencyData = (
  queryName = 'currencyCurrentDate',
  enabled: boolean,
  fieldKey: CurrencyFields,
  sortOrder: TableOrder,
  searchValue: string,
) => {
  return useQuery([queryName, searchValue, fieldKey, sortOrder], getFilteredAndSortedCurrencyData, {
    enabled,
    keepPreviousData: true,
  });
};
