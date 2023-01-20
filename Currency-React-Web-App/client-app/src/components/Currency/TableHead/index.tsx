import React from 'react';
import TableHeadSort from '../../Tables/TableHeadSort';
import { ITableHead } from '../types';

export const TableHead = ({ tableHeadConfig, ...restProps }: ITableHead) => (
  <thead>
    <tr>
      {tableHeadConfig.map((headItem) => (
        <TableHeadSort key={headItem.title} title={headItem.title} fieldKey={headItem.fieldKey} {...restProps} />
      ))}
    </tr>
  </thead>
);
