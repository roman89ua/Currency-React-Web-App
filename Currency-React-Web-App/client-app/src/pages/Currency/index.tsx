import React, { ChangeEvent, MutableRefObject, useCallback, useEffect, useRef, useState } from 'react';
import { Col, Container, FloatingLabel, Form, Row, Table } from 'react-bootstrap';
import { debounce } from 'lodash';
import PageSpinner from '../../components/PageSpinner';
import { TableHead, TableBody, CurrencyTableHeadConfig, ICurrency } from '../../components/Currency';
import { useSearchCurrencyData } from '../../queryHooks/Currency';
import { TableOrder } from '../../components/Tables/TableHeadSort/Enums';
import { CurrencyFields } from '../../components/Tables/TableHeadSort/types';
import { useQueryClient } from 'react-query';
import { AxiosResponse } from 'axios';

const QUERY_NAME = 'currencyCurrentDate';

const Currency = () => {
  const queryClient = useQueryClient();

  const cachedData = queryClient
    .getQueryCache()
    .getAll()
    .filter((query) => {
      const queryData = query.state.data as AxiosResponse<ICurrency[], unknown>;

      return query.queryKey.includes(QUERY_NAME) && !!queryData?.data?.length;
    })
    .at(-1);

  const cachedDefaultSearchValue = cachedData?.queryKey[1] as string;
  const cachedFieldName = cachedData?.queryKey[2] as CurrencyFields;
  const cachedSortOrder = cachedData?.queryKey[3] as TableOrder;

  const searchCurrencyRef: MutableRefObject<HTMLInputElement | null> = useRef<HTMLInputElement | null>(null);

  const [searchValue, setSearchValue] = useState<string>(cachedDefaultSearchValue || '');
  const [sortFieldName, setSortFieldName] = useState<CurrencyFields>(cachedFieldName || CurrencyFields.Text);
  const [sortOrder, setSortOrder] = useState<TableOrder>(cachedSortOrder || TableOrder.Descending);
  const [wasLoadedFistTime, setWasLoadedFirstTime] = useState(false);
  const sortData = (order: TableOrder, fieldKey: CurrencyFields) => {
    setSortOrder(order === TableOrder.Ascending ? TableOrder.Descending : TableOrder.Ascending);
    setSortFieldName(fieldKey);
  };

  const onInputChange = (e: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const value = e.target.value.trim();
    setSearchValue(value);
  };

  useEffect(() => {
    searchCurrencyRef.current?.focus();
    setWasLoadedFirstTime(true);
  }, []);

  useEffect(() => {
    wasLoadedFistTime && search();
  }, [searchValue, sortFieldName, sortOrder]);

  const { isLoading, refetch, data } = useSearchCurrencyData(
    QUERY_NAME,
    !cachedData,
    sortFieldName,
    sortOrder,
    searchValue,
  );

  const search = useCallback(
    debounce(() => {
      return refetch();
    }, 1500),
    [],
  );

  return (
    <Container>
      <Row>
        <Col>
          <h2>Current day currency</h2>

          <Form>
            <FloatingLabel controlId="floatingInput" label="Filter" className="my-4">
              <Form.Control
                ref={searchCurrencyRef}
                type="text"
                placeholder="Filter"
                onChange={onInputChange}
                value={searchValue}
              />
            </FloatingLabel>
          </Form>

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
