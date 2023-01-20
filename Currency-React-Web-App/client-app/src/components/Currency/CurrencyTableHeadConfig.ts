import { CurrencyFields } from '../Tables/TableHeadSort/types';
import { CurrencyTableHeadConfigType } from './types';

export const CurrencyTableHeadConfig: CurrencyTableHeadConfigType = [
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
