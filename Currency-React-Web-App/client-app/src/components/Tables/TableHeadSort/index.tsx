import React, { useState } from 'react';
import SortToggleBtn from '../../Buttons/SortToggleBtn';
import { ICurrency } from '../../Currency';
import { CurrencyFields } from './types';
import { TableOrder } from './Enums';

interface ITableHeadSort {
  setCurrency: React.Dispatch<React.SetStateAction<ICurrency[]>>;
  title: string;
  fieldKey: CurrencyFields;
}

const TableHeadSort = ({ setCurrency, title, fieldKey }: ITableHeadSort) => {
  const [sortOrder, setSortOrder] = useState<TableOrder>(TableOrder.Ascending);

  const onHandleClick = async (objKey: CurrencyFields, order: TableOrder) => {
    const response = await fetch(`currencyCurrentDate/sortCurrencyData/${objKey}/${order}`);
    const data = await response.json();

    if (data) {
      setCurrency(data);
      setSortOrder(order === TableOrder.Ascending ? TableOrder.Descending : TableOrder.Ascending);
    }
  };

  return (
    <th scope="col">
      <SortToggleBtn title={title} onClick={() => onHandleClick(fieldKey, sortOrder)} />
    </th>
  );
};

export default TableHeadSort;
