import React, { FC, Fragment, useEffect, useRef, useState } from 'react';
import { useForm } from 'react-hook-form';
import {
  Button,
  Card,
  FormControl,
  FormLabel,
  FormGroup,
  Modal,
  Tooltip, Dialog, CardContent, CardHeader, Input, Select, TextField,
} from '@material-ui/core';
import axios from 'axios';
import CreateCategory from '../category/CreateCategory';
import { CATEGORY_URL, INVENTORY_URL } from '../../constants';
import {CategoryInterface, CreateInventoryProp} from "../../types/supplierTypes";
import {CreateInventory  } from "../../utils/apiService";

interface formData {
  data: CreateInventoryProp;
  categories: CategoryInterface[];
}

export const AddInventory: FC<formData> = ({ categories }) => {
  const { register, handleSubmit, errors } = useForm<CreateInventoryProp>();
  // TODO: FIX MODAL FOR CATEGORY CREATION
  const [dropDown, setDropDown] = useState([]);
  const currentCategories = useRef(categories);
  const [modal, setModal] = useState(false);
  const showModal = () => setModal(!modal);

  useEffect(() => {
    let mounted = true;
    const result = async () => {
      const response = await axios.get(CATEGORY_URL);
      if (mounted) setDropDown(response.data);
    };
    result();

    return () => {
      mounted = false;
    };
  }, []);
  currentCategories.current = dropDown;

  
  return (
    <Fragment>
      <Card className="shadow-lg mb-5 bg-white rounded">
        <CardContent>
        <CardHeader as="h3" className="text-center">
          Create Inventory
        </CardHeader>
          <form onSubmit={handleSubmit(CreateInventory)}>
            <FormGroup>
              <FormLabel>
                Inventory Name <span className="text-danger">*</span>
              </FormLabel>
              <Input
                id="inventoryName"
                type="text"
                name="name"
                placeholder="Inventory name..."
                ref={register({
                  required: 'You have to give this inventory a name',
                  pattern: /[a-zA-Z0-9]/,
                })}
              />
              {errors.name ? (
                <span className="text-danger">{errors.name.message}</span>
              ) : null}
            </FormGroup>
            <FormGroup>
              <FormLabel>
                Supply Date <span className="text-danger">*</span>
              </FormLabel>
              <Input
                id="supplyDate"
                type="date"
                name="supplyDate"
                ref={register({
                  required: 'Please add a date before submitting!',
                })}
              />
              {errors.supplyDate ? (
                <span className="text-danger">{errors.supplyDate.message}</span>
              ) : null}
            </FormGroup>
            <FormGroup>
              <FormLabel>
                Batch Number <span className="text-danger">*</span>
              </FormLabel>
              <Input
                id="batchNumber"
                type="text"
                name="batchNumber"
                placeholder="Inventory BatchNumber..."
                ref={register({
                  required:
                    'A batch number is required for the inventory to be created!',
                })}
              />
              {errors.batchNumber ? (
                <span className="text-danger">
                  {errors.batchNumber.message}
                </span>
              ) : null}
            </FormGroup>
            <FormGroup inlist="true">
                  <FormLabel>
                    Inventory Category <span className="text-danger">*</span>
                  </FormLabel>
                  <Select
                    name="categoryName"
                    ref={register({ required: true })}
                    className="py-2"
                  >
                    {currentCategories &&
                      currentCategories.current.map((value) => (
                        <option key={value.categoryId}>
                          {value.categoryName}
                        </option>
                      ))}
                  </Select>
                  {errors.categoryName ? (
                    <span className="text-danger">
                      {errors.categoryName.message}
                    </span>
                  ) : null}
                
            </FormGroup>
            <FormGroup>
              <FormLabel>
                Product Quantity <span className="text-danger">*</span>
              </FormLabel>
              <Input
                name="productQuantity"
                type="number"
                ref={register({
                  required: 'You must enter a number',
                  pattern: /[0-9]/,
                })}
              />
              {errors.productQuantity ? (
                <span className="text-danger">
                  {errors.productQuantity.message}
                </span>
              ) : null}
            </FormGroup>

            <FormGroup>
              <FormLabel>
                Description <span className="text-danger">*</span>
              </FormLabel>
              <Input
                id="inventoryDescription"
                multiline
                name="description"
                placeholder="Inventory Description..."
                inputRef={register({
                  required: 'Please add a description for this inventory!',
                })}
              />
              {errors.description ? (
                <span className="text-danger">
                  {errors.description.message}
                </span>
              ) : null}
            </FormGroup>
            <FormGroup>
              <Button type="submit" color="primary">
                <span>
                  <b>Create</b>
                </span>
              </Button>
            </FormGroup>
          </form>
        </CardContent>
      </Card>

      {/*<div>*/}
      {/*  <Dialog show={!showModal} onHide={showModal}>*/}
      {/*    <Modal.Header closeButton>*/}
      {/*      <Modal.Title>ADD</Modal.Title>*/}
      {/*    </Modal.Header>*/}
      {/*    <ModalBody>*/}
      {/*      <CreateCategory />*/}
      {/*    </ModalBody>*/}
      {/*  </Dialog>*/}
      {/*</div>*/}
    </Fragment>
  );
};

