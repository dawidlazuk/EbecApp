import { OrderStatus } from "./order-status.enum";
import { IProductType } from "./product/productType";

export interface IOrder{
    id: number;
    status: OrderStatus;
    products: {productType: IProductType, amount: number}[];
    value: number;
    createdDate: Date;
    modifiedDate: Date;
}

export class Order implements IOrder {
    id: number;
    status: OrderStatus;
    products: { productType: IProductType; amount: number; }[];
    value: number;
    createdDate: Date;
    modifiedDate: Date;

}
