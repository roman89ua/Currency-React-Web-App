import React from 'react';
import { ICurrency } from '../index';

export const TableBody = ({ currency }: { currency: ICurrency[] }) => {
  if (!currency.length) return null;

  return (
    <tbody>
      {currency.map((cur, index) => {
        return (
          <tr key={cur.id}>
            <th scope="row">{index + 1}</th>
            <td>{cur.text}</td>
            <td>{cur.currency}</td>
            <td>{cur.rate}</td>
            <td>{cur.exchangeDate}</td>
          </tr>
        );
      })}
    </tbody>
  );
};
