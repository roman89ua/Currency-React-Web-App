import React, { useState } from 'react';
import SortToggleBtn from '../../Buttons/SortToggleBtn';
import { TableOrder } from './Enums';
import { ICurrency, ITableHeadSort } from '../../Currency';
import { AxiosRequestConfig, AxiosResponse } from 'axios';
import { useSortedCurrencyData } from '../../../queryHooks/Curency/useSortedCurrencyData';

const TableHeadSort = ({ setCurrency, title, fieldKey }: ITableHeadSort) => {
  const [sortOrder, setSortOrder] = useState<TableOrder>(TableOrder.Descending);

  if (!setCurrency || !fieldKey) {
    return <th scope="col">{title}</th>;
  }
  const onSuccess = (data: AxiosResponse<ICurrency[], AxiosRequestConfig>) => {
    setCurrency(data.data);
  };

  const sortedCurrencyData = useSortedCurrencyData(fieldKey, sortOrder, onSuccess, false);

  const sortData = async (order: TableOrder) => {
    await sortedCurrencyData.refetch();
    setSortOrder(order === TableOrder.Ascending ? TableOrder.Descending : TableOrder.Ascending);
  };

  return (
    <th scope="col">
      <SortToggleBtn title={title} onClick={() => sortData(sortOrder)} />
    </th>
  );
};

export default TableHeadSort;
