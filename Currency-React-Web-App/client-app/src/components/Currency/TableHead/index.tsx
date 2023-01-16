import React from 'react';
import TableHeadSort from '../../Tables/TableHeadSort';
import { ITableHead } from '../types';

export const TableHead = ({ tableHeadConfig, setterFunc }: ITableHead) => (
  <thead>
    <tr>
      {tableHeadConfig.map((headItem) => (
        <TableHeadSort
          key={headItem.title}
          title={headItem.title}
          setCurrency={setterFunc}
          fieldKey={headItem.fieldKey}
        />
      ))}
    </tr>
  </thead>
);
