import React from 'react';
import { ICurrency } from '../index';
import { Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';

export const TableBody = ({ currency }: { currency: ICurrency[] }) => {
  if (!currency.length) return null;

  return (
    <tbody>
      {currency.map((cur, index) => {
        return (
          <tr key={cur.id}>
            <th scope="row" className="align-middle">
              {index + 1}
            </th>
            <td className="align-middle">{cur.text}</td>
            <td className="align-middle">{cur.currency}</td>
            <td className="align-middle">{cur.rate}</td>
            <td className="align-middle">{cur.exchangeDate}</td>
            <td className="align-middle">
              <Link to={`/currency/${cur.currency}`}>
                <Button variant="outline-secondary">Chart</Button>
              </Link>
            </td>
          </tr>
        );
      })}
    </tbody>
  );
};
