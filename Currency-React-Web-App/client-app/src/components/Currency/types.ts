import { Dispatch, SetStateAction } from 'react';
import { CurrencyFields } from '../Tables/TableHeadSort/types';

export interface ICurrency {
  id: string;
  text: string;
  currency: string;
  rate: string;
  exchangeDate: string;
}
export interface ITableHeadSort {
  title: string;
  setCurrency?: Dispatch<SetStateAction<ICurrency[]>>;
  fieldKey?: CurrencyFields;
}

export interface ITableHead {
  tableHeadConfig: Omit<ITableHeadSort, 'setCurrency'>[];
  setterFunc: Dispatch<SetStateAction<ICurrency[]>>;
}
