import React from "react";
import axios from "axios";
import {BASE_URL, INVENTORY_URL, SUPPLIERS_URL} from "../constants";
import {CreateInventoryProp, SupplierResponseType} from "../types/supplierTypes";

/*
 * Handle Inventory creation on the server. Expect
 */
export const CreateInventory = (
    data: CreateInventoryProp,
    evt: React.BaseSyntheticEvent<object, any, any> | undefined
) => {
    try {
        axios
            .post(`${INVENTORY_URL}`, data, {
                headers: {
                    'Content-Type': 'application/json',
                },
            })
            .then((response) => {
                console.log(response);
                if (response.status >= 200 && response.status < 300 && response.data.currentResponseStatus) {
                    // @ts-ignore
                    evt.target.reset();
                }
            });
    } catch (error) {
        console.log('error on submitting inventory');
    }
};

export async function getSupplier(): Promise<any> {
    const result = (await axios.get(`${SUPPLIERS_URL}`)).data.data;
    return result;
}