import React, {useEffect, useState} from "react";
import {Col, Container, Row, Spinner, Table} from "react-bootstrap";
import SortToggleBtn from "./Buttons/SortToggleBtn";
import TableHeadSort from "./Tables/TableHeadSort";

const Currency = () => {
  const [currency, setCurrency] = useState(null);
  const [isLoading, setIsLoading] = useState(false);

  async function populateCurrencyData() {
    setIsLoading(true);
    const response = await fetch('currencycurrentdate');
    const data = await response.json();
    setCurrency(data);
    if (!!data) {
      setIsLoading(false);
    }
  }

  useEffect(() => {
    populateCurrencyData();
  }, []);

  return (
    <Container>
      <Row>
        <Col>
          <h2>Current day currency</h2>

          {isLoading
            ? (<div className="w-100 py-5 d-flex justify-content-center">
              <Spinner animation="border"/>
            </div>)
            : (
              <div className="table-responsive">
                {!!currency && (
                  <Table className="table-striped">
                    <thead>
                    <tr>
                      <th scope="col">#</th>
                      <TableHeadSort title="Name" setCurrency={setCurrency} fieldKey="Text"/>
                      <TableHeadSort title="Currency code" setCurrency={setCurrency} fieldKey="Currency"/>
                      <TableHeadSort title="Rate(UAH)" setCurrency={setCurrency} fieldKey="Rate"/>
                      <TableHeadSort title="Date" setCurrency={setCurrency} fieldKey="ExchangeDate"/>
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
                      )
                    })}
                    </tbody>
                  </Table>
                )}
              </div>
            )
          }
        </Col>
      </Row>
    </Container>
  )
};

export default Currency;
