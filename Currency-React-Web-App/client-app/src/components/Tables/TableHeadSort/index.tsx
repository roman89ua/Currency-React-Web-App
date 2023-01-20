import React from 'react';
import SortToggleBtn from '../../Buttons/SortToggleBtn';
import { ITableHeadSort } from '../../Currency';

const TableHeadSort = ({ title, fieldKey, onDataSort, currentOrder }: ITableHeadSort) => {
  if (!fieldKey) {
    return <th scope="col">{title}</th>;
  }

  return (
    <th scope="col">
      <SortToggleBtn title={title} onClick={() => onDataSort(currentOrder, fieldKey)} />
    </th>
  );
};

export default TableHeadSort;
