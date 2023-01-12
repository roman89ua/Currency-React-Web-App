import React, { useCallback, useEffect, useState } from 'react';
import { Col, Container, FloatingLabel, Form, FormGroup, Row, Spinner, Table } from 'react-bootstrap';
import { debounce } from 'lodash';
import TableHeadSort from '../Tables/TableHeadSort';

export interface ICurrency {
  id: string;
  text: string;
  currency: string;
  rate: string;
  exchangeDate: string;
}

const Currency = () => {
  const [currency, setCurrency] = useState<ICurrency[]>([]);
  const [isLoading, setIsLoading] = useState(false);

  async function populateCurrencyData() {
    setIsLoading(true);
    const response = await fetch('currencyCurrentDate');
    const data = await response.json();
    setCurrency(data);
    if (data) {
      setIsLoading(false);
    }
  }

  useEffect(() => {
    populateCurrencyData();
  }, []);

  const onInputChange = debounce(
    useCallback(async (e) => {
      setIsLoading(true);
      const value = e.target.value.toString().trim();
      const response = await fetch(`currencyCurrentDate/filterCurrencyData/${value ? value : null}`);
      const data = await response.json();
      if (data) {
        setCurrency(data);
        setIsLoading(false);
      }
    }, []),
    1250,
  );

  return (
    <Container>
      <Row>
        <Col>
          <h2>Current day currency</h2>

          <FormGroup>
            <FloatingLabel controlId="floatingInput" label="Filter" className="my-4">
              <Form.Control type="text" placeholder="Filter" onChange={(e) => onInputChange(e)} />
            </FloatingLabel>
          </FormGroup>

          {isLoading ? (
            <div className="w-100 py-5 d-flex justify-content-center">
              <Spinner animation="border" />
            </div>
          ) : (
            <div className="table-responsive">
              {!!currency && (
                <Table className="table-striped">
                  <thead>
                    <tr>
                      <th scope="col">#</th>
                      <TableHeadSort title="Name" setCurrency={setCurrency} fieldKey="Text" />
                      <TableHeadSort title="Code" setCurrency={setCurrency} fieldKey="Currency" />
                      <TableHeadSort title="Rate" setCurrency={setCurrency} fieldKey="Rate" />
                      <TableHeadSort title="Date" setCurrency={setCurrency} fieldKey="ExchangeDate" />
                    </tr>
                  </thead>
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
                </Table>
              )}
            </div>
          )}
        </Col>
      </Row>
    </Container>
  );
};

export default Currency;
