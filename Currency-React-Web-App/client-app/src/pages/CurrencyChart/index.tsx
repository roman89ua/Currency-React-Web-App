import React, { useCallback, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { Button, Col, Row } from 'react-bootstrap';
import { useCurrencyRateHistoryData } from '../../queryHooks/Currency/useCurrencyRateHistoryData';
import ReactDatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';
import moment from 'moment/moment';

import { LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend, ResponsiveContainer } from 'recharts';

const CurrencyChart = () => {
  const { currencyCode } = useParams();

  const currentDate = new Date();
  const firstDayOfTheYear = new Date(`01.01.${currentDate.getFullYear()}`);

  const [startDate, setStartDate] = useState<Date | null>(firstDayOfTheYear);
  const [endDate, setEndDate] = useState<Date | null>(currentDate);
  const isRefetchPossible = !!startDate && !!endDate;
  const navigate = useNavigate();

  const dateFormatter = (date: Date | null): string => {
    return moment(date).format('DD.MM.YYYY') || '';
  };

  const { data, refetch } = useCurrencyRateHistoryData(
    dateFormatter(startDate),
    dateFormatter(endDate),
    currencyCode || '',
    isRefetchPossible,
  );

  const goBack = (): void => {
    navigate(-1);
  };

  const onDateChange = (dates: [Date | null, Date | null]) => {
    setStartDate(dates?.at(0) || null);
    setEndDate(dates?.at(1) || null);
  };

  const [opacity, setOpacity] = useState({
    exchangeDate: 1,
    rate: 1,
  });
  const handleMouseEnter = useCallback(
    (o: any) => {
      const { dataKey } = o;

      setOpacity({ ...opacity, [dataKey]: 0.5 });
    },
    [opacity, setOpacity],
  );

  const handleMouseLeave = useCallback(
    (o: any) => {
      const { dataKey } = o;
      setOpacity({ ...opacity, [dataKey]: 1 });
    },
    [opacity, setOpacity],
  );

  useEffect(() => {
    isRefetchPossible && refetch();
  }, [startDate, endDate]);

  return (
    <Row>
      <Col xs={12}>
        <Button onClick={goBack}>Go Back</Button>
      </Col>
      <Col xs={12}>
        <h1 className="text-center">
          Ukrainian Hryvnya(UAH) rate to {data?.data.at(0)?.englishName}({data?.data.at(0)?.currency})
        </h1>
      </Col>
      <Col xs={12} className="d-flex flex-column align-items-center">
        <ReactDatePicker
          maxDate={currentDate}
          placeholderText="Select dates"
          startDate={startDate}
          endDate={endDate}
          dateFormat={'dd.MM.yyyy'}
          selectsRange={true}
          onChange={onDateChange}
          withPortal
        />
      </Col>
      <Col xs={12} className="d-flex flex-column align-items-center">
        <ResponsiveContainer width="100%" height={400}>
          <LineChart
            width={700}
            height={300}
            data={data?.data}
            margin={{
              top: 20,
              right: 0,
              left: 0,
              bottom: 20,
            }}
          >
            <CartesianGrid strokeDasharray="10 10" />
            <XAxis dataKey="exchangeDate" />
            <YAxis name="UAH" />
            <Tooltip />
            <Legend onMouseEnter={handleMouseEnter} onMouseLeave={handleMouseLeave} />

            <Line
              name={`${data?.data.at(0)?.englishName}, ${data?.data.at(0)?.currency}`}
              type="monotone"
              dataKey="ratePerUnit"
              strokeOpacity={opacity.rate}
              stroke="#82ca9d"
              strokeWidth={2}
            />
          </LineChart>
        </ResponsiveContainer>
      </Col>
    </Row>
  );
};

export default CurrencyChart;
