import React, { FC } from "react";
import { useForm } from "react-hook-form";
import axios from "axios";
import {Button, Card, CardContent, FormControl, FormGroup, FormLabel, Input, Typography} from "@material-ui/core";
interface createCategoryProp {
  name?: string;
  description?: string;
}

const CreateCategory: FC<createCategoryProp> = () => {
  const { register, handleSubmit } = useForm<createCategoryProp>();
  return (
    <>
      <Card>
        <CardContent>
          <Typography color="primary" variant={"h3"} className="text-center font-weight-bold">
            Create Category
          </Typography>
          <hr/>
          <form
            onSubmit={handleSubmit(async (data, evt) => {
              await axios.post("https://localhost:5001/api/categories", data);
              // @ts-ignore
              evt.target.reset();
            })}
          >
            <FormGroup>
              <FormLabel>Category Name</FormLabel>
              <Input
                name="name"
                type="text"
                id="categoryName"
                placeholder="a product or inventory category name..."
                ref={register({
                  required: true,
                  maxLength: 50,
                  minLength: 2,
                })}
              />
            </FormGroup>

            <FormGroup>
              <FormLabel>Category Description</FormLabel>
              <Input
                name="description"
                type="textarea"
                id="categoryDescription"
                placeholder=" inventory category description..."
                ref={register({ required: false, maxLength: 200 })}
              />
            </FormGroup>

            <FormGroup>
              <Button type="submit" color="primary">
                <span>
                  <b>Create Category</b>
                </span>
              </Button>
            </FormGroup>
          </form>
        </CardContent>
      </Card>
    </>
  );
};

export default CreateCategory;
