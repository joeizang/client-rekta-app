import React, { Fragment } from 'react';
import { COLOURS } from '../../constants';
import { useForm } from 'react-hook-form';
import { Card, CardContent, CardHeader, FormGroup, TextField } from '@material-ui/core';

interface ProductForm {
  productName: string;
}

export function CreateProduct() {
  const { register, handleSubmit, errors } = useForm<IProductForm>();

  return (
    <Fragment>
      <Card className="shadow-sm mb-5 bg-white rounded">
        <CardHeader as="h3" style={{ backgroundColor: COLOURS.primary, color: 'whitesmoke' }}>
          Add a New Product
        </CardHeader>
        <CardContent>
          <form>
            <FormGroup>
              <TextField
                variant={'outlined'}
                type="text"
                id="productName"
                innerRef={register({
                  required: 'Please provide a name for the product!',
                  maxLength: 50,
                })}
              />
              {errors.productName && <span style={{ color: 'red' }}>{errors.productName.message}</span>}
            </FormGroup>
          </form>
        </CardContent>
      </Card>
    </Fragment>
  );
}
