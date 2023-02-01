import React, { ChangeEvent, useCallback, useEffect, useState } from 'react';
import { Col, Container, FloatingLabel, Form, FormGroup, Row, Table } from 'react-bootstrap';
import { debounce } from 'lodash';
import PageSpinner from '../../components/PageSpinner';
import { TableHead, TableBody, CurrencyTableHeadConfig } from '../../components/Currency';
import { useSearchCurrencyData } from '../../queryHooks/Currency';
import { TableOrder } from '../../components/Tables/TableHeadSort/Enums';
import { CurrencyFields } from '../../components/Tables/TableHeadSort/types';
import { useQueryClient } from 'react-query';

/*
  "null" is the value which is excepted by BE === means no filter value
*/
export const DEFAULT_SEARCH_CURRENCY_VALUE = 'null';
const QUERY_NAME = 'currencyCurrentDate';

const Currency = () => {
  const queryClient = useQueryClient();
  const [searchValue, setSearchValue] = useState<string>(DEFAULT_SEARCH_CURRENCY_VALUE);
  const [sortOrder, setSortOrder] = useState<TableOrder>(TableOrder.Descending);
  const [sortFieldName, setSortFieldName] = useState<CurrencyFields>(CurrencyFields.Text);

  const { isLoading, refetch, data } = useSearchCurrencyData(QUERY_NAME, false, sortFieldName, sortOrder, searchValue);

  const sortData = (order: TableOrder, fieldKey: CurrencyFields) => {
    setSortOrder(order === TableOrder.Ascending ? TableOrder.Descending : TableOrder.Ascending);
    setSortFieldName(fieldKey);
  };

  const onInputChange = debounce(
    useCallback(async (e: ChangeEvent<HTMLInputElement>) => {
      const value = e.target.value.trim() || DEFAULT_SEARCH_CURRENCY_VALUE;
      if (value) {
        setSearchValue(value);
      }
    }, []),
    1250,
  );

  useEffect(() => {
    const DATA = queryClient
      .getQueryCache()
      .getAll()
      .filter((query) => query.queryKey.includes(QUERY_NAME))
      .at(-1);
    console.log(DATA);
    if (DATA) {
      const cachedSearchValue = DATA?.queryKey[1];
      const cachedSortFieldName = DATA?.queryKey[2];
      const cachedSortOrder = DATA?.queryKey[3];

      setSearchValue(cachedSearchValue as string);
      setSortOrder(cachedSortOrder as TableOrder);
      setSortFieldName(cachedSortFieldName as CurrencyFields);
    }
  }, []);

  useEffect(() => {
    searchValue && sortFieldName && sortOrder && refetch();
  }, [searchValue, sortFieldName, sortOrder]);

  return (
    <Container>
      <Row>
        <Col>
          <h2>Current day currency</h2>

          <FormGroup>
            <FloatingLabel controlId="floatingInput" label="Filter" className="my-4">
              <Form.Control type="text" placeholder="Filter" onChange={onInputChange} />
            </FloatingLabel>
          </FormGroup>

          {isLoading ? (
            <PageSpinner />
          ) : (
            <div className="table-responsive">
              <Table className="table-striped">
                <TableHead tableHeadConfig={CurrencyTableHeadConfig} onDataSort={sortData} currentOrder={sortOrder} />
                <TableBody currency={data?.data || []} />
              </Table>
            </div>
          )}
        </Col>
      </Row>
    </Container>
  );
};

export default Currency;
