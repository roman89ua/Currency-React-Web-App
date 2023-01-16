import React, { ChangeEvent, memo, useCallback, useEffect, useState } from 'react';
import { Col, Container, FloatingLabel, Form, FormGroup, Row, Table } from 'react-bootstrap';
import { debounce } from 'lodash';
import PageSpinner from '../../components/PageSpinner';
import { TableHead, TableBody, ICurrency, CurrencyTableHeadConfig } from '../../components/Currency';
import { useCurrentDataCurrency, useFilteredCurrencyData } from '../../queryHooks/Curency';
import { AxiosRequestConfig, AxiosResponse } from 'axios';

const Currency = () => {
  const [searchValue, setSearchValue] = useState<string>('');

  const [currency, setCurrency] = useState<ICurrency[]>([]);
  const onSuccess = (data: AxiosResponse<ICurrency[], AxiosRequestConfig>) => setCurrency(data.data);

  const loadCurrencyList = useCurrentDataCurrency(onSuccess, false);

  const searchForCurrencyResponse = useFilteredCurrencyData(false, searchValue, onSuccess);

  const onInputChange = debounce(
    useCallback(async (e: ChangeEvent<HTMLInputElement>) => {
      /*
        "null" is the value which is excepted by BE === means no filter value
        as empty string not fit for endpoint param
      */
      const value = e.target.value.toString().trim() || 'null';
      if (value) {
        setSearchValue(value);
      }
    }, []),
    1250,
  );

  useEffect(() => {
    searchValue && searchForCurrencyResponse.refetch();
  }, [searchValue]);

  return (
    <Container>
      <Row>
        <Col>
          <h2>Current day currency</h2>

          <FormGroup>
            <FloatingLabel controlId="floatingInput" label="Filter" className="my-4">
              <Form.Control type="text" placeholder="Filter" onChange={onInputChange} value={searchValue} />
            </FloatingLabel>
          </FormGroup>

          {loadCurrencyList.isLoading || searchForCurrencyResponse.isLoading ? (
            <PageSpinner />
          ) : (
            <div className="table-responsive">
              <Table className="table-striped">
                <TableHead tableHeadConfig={CurrencyTableHeadConfig} setterFunc={setCurrency} />
                <TableBody currency={currency} />
              </Table>
            </div>
          )}
        </Col>
      </Row>
    </Container>
  );
};

export default memo(Currency);
