import React, { AllHTMLAttributes, FormEvent, FormEventHandler, useState } from 'react';
// import { api } from '../../api';
import { Button, Col, Form, FormControlProps, Row } from 'react-bootstrap';
import { FormInput } from '../../components/Forms';

const Login = () => {
  const [formValues, setDFormValues] = useState({
    email: '',
    password: '',
  });

  const inputs: Array<AllHTMLAttributes<HTMLLabelElement & HTMLInputElement> & FormControlProps> = [
    {
      id: '1',
      name: 'email',
      type: 'text',
      placeholder: 'Enter email',
      label: 'Email address',
    },
    {
      id: '2',
      name: 'password',
      type: 'text',
      placeholder: 'Password',
      label: 'Password',
    },
  ];
  // useEffect(() => {
  //   api
  //     .get('/applogin')
  //     .then((response) => console.log(response.data))
  //     .catch((error) => console.log(error.message));
  // }, []);

  const formClear: FormEventHandler<HTMLFormElement> = (e: FormEvent<HTMLFormElement>) => {
    e.currentTarget.reset();
  };
  const onFormSubmit: FormEventHandler<HTMLFormElement> = (e) => {
    e.preventDefault();
    const data = new FormData(e.currentTarget);
    console.log(e);
    console.log(data);

    const result = Object.fromEntries(data.entries());
    console.log('result: ', result);
  };
  return (
    <Row className="flex-grow-1 d-flex justify-content-center align-items-center">
      <Col sm={12} md={12} lg={6} xl={4} className="d-flex flex-column justify-content-center ">
        <h1>Login</h1>
        <Form onSubmit={onFormSubmit} onReset={formClear}>
          {inputs.map((input) => {
            return <FormInput key={input.id} {...input} />;
          })}
          <Row>
            <Col sm={6} className="py-1">
              <Button className="w-100" variant="outline-primary" type="submit">
                Sign in
              </Button>
            </Col>
            <Col sm={6} className="py-1">
              <Button className="w-100" variant="outline-warning" type="reset">
                Clear
              </Button>
            </Col>
          </Row>
        </Form>
      </Col>
    </Row>
  );
};

export default Login;
