import { OrderStatus } from "./order-status.enum";
import { IProductType } from "./product/productType";

export interface IOrder{
    status: OrderStatus;
    productsInCart: {productType: IProductType, amount: number}[];
    value: number;
    createdDate: Date;
    modifiedDate: Date;
}

export class Order implements IOrder {
    status: OrderStatus;
    productsInCart: { productType: IProductType; amount: number; }[];
    value: number;
    createdDate: Date;
    modifiedDate: Date;

}
