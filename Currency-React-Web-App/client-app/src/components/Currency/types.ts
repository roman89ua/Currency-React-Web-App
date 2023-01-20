import { CurrencyFields } from '../Tables/TableHeadSort/types';
import { TableOrder } from '../Tables/TableHeadSort/Enums';

export interface ICurrency {
  id: string;
  text: string;
  currency: string;
  rate: string;
  exchangeDate: string;
}
export interface ITableHeadSort {
  title: string;
  fieldKey?: CurrencyFields;
  currentOrder: TableOrder;
  onDataSort: (order: TableOrder, fieldKey: CurrencyFields) => void;
}

export interface ITableHead {
  tableHeadConfig: CurrencyTableHeadConfigType;
  onDataSort: (order: TableOrder, fieldKey: CurrencyFields) => void;
  currentOrder: TableOrder;
}

export type CurrencyTableHeadConfigType = Omit<ITableHeadSort, 'onDataSort' | 'setSortFieldName' | 'currentOrder'>[];
