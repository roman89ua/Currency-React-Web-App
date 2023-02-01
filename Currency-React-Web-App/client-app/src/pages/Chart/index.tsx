import React from 'react';
import { useLocation, useNavigate, useParams } from 'react-router-dom';
import { Button } from 'react-bootstrap';
import { useCurrencyRateHistoryData } from '../../queryHooks/Currency/useCurrencyRateHistoryData';
// import { useQueryClient } from 'react-query';

const Chart = () => {
  const { currencyCode } = useParams();
  const navigate = useNavigate();
  const location = useLocation();
  const { data } = useCurrencyRateHistoryData({
    startDate: '2022.12.01',
    endDate: '2022.12.31',
    currencyCode: currencyCode || '',
  });
  const goBack = (): void => {
    navigate(-1);
  };

  return (
    <>
      <Button onClick={goBack}>Go Back</Button>
      <div>Chart Page. Currency ID: {currencyCode}</div>);
    </>
  );
};

export default Chart;
