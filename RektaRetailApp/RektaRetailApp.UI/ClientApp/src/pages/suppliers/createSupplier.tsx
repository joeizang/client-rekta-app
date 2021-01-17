import React, { FC, Fragment, useState } from 'react';
import {
  Button,
  Card, CardContent, CardHeader,
  FormControl,
  FormGroup,
  FormLabel, Input, TextField, Typography,
} from '@material-ui/core';
import { useForm } from 'react-hook-form';
import { Redirect } from 'react-router-dom';
import { ICreateSupplier } from '../../types/supplierTypes';
import { createSupplier } from '../../utils/formProcessors/supplierFormProcessors';

export const CreateSupplier: FC<ICreateSupplier> = () => {
  const { register, handleSubmit, errors } = useForm<ICreateSupplier>();
  const [redirect, setRedirect] = useState(false);
  const [location, setLocation] = useState('');
  const doSubmit = (
    data: ICreateSupplier,
    evt: React.BaseSyntheticEvent<object, any, any> | undefined
  ) => {
    const result = createSupplier(data, evt);
    setRedirect(true);
    setLocation(result);
  };

  return (
    <Fragment>
      {redirect && <Redirect to={location} />}
      <Card className="shadow mb-5 bg-white rounded">
        <CardHeader
          as="h2"
          style={{ backgroundColor: '#1b3e1d', color: 'whitesmoke' }}
        >
          Add A Supplier
        </CardHeader>
        <CardContent>
          <form onSubmit={handleSubmit(doSubmit)}>
            <FormGroup>
              <FormLabel>
                <b>Supplier Name</b>{' '}
                <span style={{ color: 'red' }}>
                  <b>*</b>
                </span>
              </FormLabel>
              <TextField
                inputRef={register({
                  required: 'You need to provide a name for the supplier.',
                  maxLength: 50,
                })}
                name="name"
                type="text"
                placeholder="Supplier Name.."
              />
              {errors.name && (
                <span style={{ color: 'red' }}>{errors.name.message}</span>
              )}
            </FormGroup>
            <FormGroup>
              <FormLabel>
                <b>Supplier Mobile Number</b>{' '}
                <span style={{ color: 'red' }}>
                  <b>*</b>
                </span>
              </FormLabel>
              <TextField
                inputRef={register({
                  required:
                    'You need to provide a phone or mobile number for the supplier.',
                  maxLength: 100,
                })}
                name="phoneNumber"
                type="tel"
                placeholder="080-1111-2222"
              />
              {errors.phoneNumber && (
                <span style={{ color: 'red' }}>
                  {errors.phoneNumber.message}
                </span>
              )}
            </FormGroup>
            <FormGroup>
              <FormLabel>
                <b>Description</b>{' '}
                <span style={{ color: 'red' }}>
                  <b>*</b>
                </span>
              </FormLabel>
              <TextField
                inputRef={register({
                  required:
                    'It would be beneficial to add a description about this supplier.',
                  maxLength: 500,
                })}
                name="description"
                multiline
                placeholder="Add notes unique to this supplier..."
              />
              {errors.description && (
                <span style={{ color: 'red' }}>
                  {errors.description.message}
                </span>
              )}
            </FormGroup>
            <FormGroup>
              <Button type="submit" color="primary">
                Create Supplier
              </Button>
            </FormGroup>
          </form>
        </CardContent>
      </Card>
    </Fragment>
  );
};
