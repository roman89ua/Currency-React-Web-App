import { ITableHeadSort } from './types';
import { CurrencyFields } from '../Tables/TableHeadSort/types';

export const CurrencyTableHeadConfig: Omit<ITableHeadSort, 'setCurrency'>[] = [
  {
    title: '#',
  },
  {
    title: 'Name',
    fieldKey: CurrencyFields.Text,
  },
  {
    title: 'Code',
    fieldKey: CurrencyFields.Currency,
  },
  {
    title: 'Rate',
    fieldKey: CurrencyFields.Rate,
  },
  {
    title: 'Date',
    fieldKey: CurrencyFields.ExchangeDate,
  },
];
