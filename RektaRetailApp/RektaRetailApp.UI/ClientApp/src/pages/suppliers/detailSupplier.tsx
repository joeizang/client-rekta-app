import React, { FC, Fragment, useEffect, useState } from 'react';
import axios from 'axios';
import { BASE_URL, COLOURS } from '../../constants';
import { useLocation } from 'react-router-dom';
import {Card} from "@material-ui/core";
import {SupplierDetailType, SupplierResponseType} from "../../types/supplierTypes";
import {getSupplier} from "../../utils/apiService";
import ReactPlayer from "react-player";

export const DetailSupplier: FC<SupplierResponseType> = () => {
  const [supplier, setSupplier] = useState<SupplierDetailType>();
  const [webcam, setWebCam] = useState<MediaStream>();
  //const { id } = useParams<>();
  const location = useLocation();
  console.log(location);

  useEffect(() => {
    //const url = `${SUPPLIERS_URL}/${id}`;
    const result = getSupplier().then(response => {
      return response
    });
    setSupplier(result)
  }, []);
  console.log(supplier);
  // const xvideo = async () => {
  //   const video = document.getElementById('samplevideo');
  //   return await navigator.mediaDevices.getUserMedia({ video: true, audio: true });
  // };
  // useEffect(() => {
  //   const doIt = async () => {
  //     return await xvideo();
  //   }
  //   const result = async () => {
  //     setWebCam(await doIt());
  //   }
  //   result();
  // })
  return (
    <Fragment>
      <Card className="shadow-sm mb-5 bg-white rounded">
        {/*<ReactPlayer url={webcam} width={200} height={200} playing config={{ file: { forceVideo: true}}}/>*/}
        {/* <Card.Header>{this.state.supplier.supplierName}</Card.Header> */}
        {/*<Card.Header*/}
        {/*  as="h2"*/}
        {/*  style={{ backgroundColor: COLOURS.primary, color: 'white' }}*/}
        {/*>*/}
        {/*  <Card.Title className="text-center">Supplier - Details</Card.Title>*/}
        {/*</Card.Header>*/}
        {/*<Card.Body>*/}
        {/*  <Card.Subtitle>{supplier?.name && supplier?.name}</Card.Subtitle>*/}
        {/*  {supplier?.suppliedProducts === undefined ||*/}
        {/*  supplier?.suppliedProducts?.length < 1*/}
        {/*    ? null*/}
        {/*    : supplier?.suppliedProducts.map((product) => (*/}
        {/*        <ListGroup key={product.id}>*/}
        {/*          <ListGroup.Item>*/}
        {/*            <Card.Text>Product Name: {product.name}</Card.Text>*/}
        {/*            <Card.Text>Quantity: {product.quantity}</Card.Text>*/}
        {/*          </ListGroup.Item>*/}
        {/*        </ListGroup>*/}
        {/*      ))}*/}
        {/*</Card.Body>*/}
        <p>Supplier Detail</p>
      </Card>
    </Fragment>
  );
};
