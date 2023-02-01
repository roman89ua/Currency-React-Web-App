import React from 'react';
import { Col, Row } from 'react-bootstrap';

const pageNotFound = () => (
  <Row className="py-5">
    <Col className="text-center">
      <h1
        style={{
          fontSize: '40px',
        }}
      >
        Page not found
      </h1>
      <p
        className="text-danger"
        style={{
          fontSize: '100px',
          fontWeight: 'bold',
          textShadow: 'rgba(173,102,102,0.7) 5px 5px',
        }}
      >
        404
      </p>
      <p className="h5">Looks like this page not exist any more.</p>
    </Col>
  </Row>
);

export default pageNotFound;
