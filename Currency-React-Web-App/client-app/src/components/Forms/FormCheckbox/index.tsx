import React, { AllHTMLAttributes, FC } from 'react';
import { Form, FormCheckProps, FormControlProps } from 'react-bootstrap';

export const FormCheckbox: FC<AllHTMLAttributes<HTMLLabelElement & HTMLInputElement> & FormCheckProps> = ({
  name,
  label,
  ...rest
}) => {
  return (
    <Form.Group className="mb-3" controlId={name}>
      <Form.Check name={name} type="checkbox" label={label} {...rest} />
    </Form.Group>
  );
};
