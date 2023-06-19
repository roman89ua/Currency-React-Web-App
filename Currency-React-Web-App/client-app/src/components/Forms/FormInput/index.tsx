import React, { AllHTMLAttributes, FC } from 'react';
import { Form, FormControlProps } from 'react-bootstrap';
interface IFormInput {
  name: string;
  label: string;
  placeholder: string;
  type: string;
}
export const FormInput: FC<AllHTMLAttributes<HTMLLabelElement & HTMLInputElement> & FormControlProps> = ({
  name,
  label,
  placeholder,
  type,
}) => {
  return (
    <Form.Group className="mb-3" controlId={name}>
      <Form.Label>{label}</Form.Label>
      <Form.Control name={name} type={type} placeholder={placeholder} />
      {/* <Form.Text className="text-muted">We`ll never share your email with anyone else.</Form.Text>*/}
    </Form.Group>
  );
};
