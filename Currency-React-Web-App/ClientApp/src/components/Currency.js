import React, {useEffect, useState} from "react";
import {Col, Container, Row, Table} from "reactstrap";

const Currency = () => {
  const [currency, setCurrency] = useState(null);

  async function populateCurrencyData() {
    const response = await fetch('currencycurrentdate');
    const data = await response.json();
    console.log(data);
    setCurrency(data);
  }

  useEffect(() => {
    populateCurrencyData();
  }, []);


  return (
    <Container>
      <Row>
        <Col>
          <h2>Current day currency</h2>
          <div className="table-responsive">
            {!!currency && (
              <Table className="table-striped">
                <thead>
                <tr>
                  <th scope="col">#</th>
                  <th scope="col">Name</th>
                  <th scope="col">Currency code</th>
                  <th scope="col">Rate(UAH)</th>
                  <th scope="col">Date</th>
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
        </Col>
      </Row>
    </Container>
  )
};

export default Currency;
