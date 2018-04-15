import { OrderStatus } from "./order-status.enum";
import { IProductType } from "../products/productType";

export interface IOrder{
    id: number;
    status: OrderStatus;
    products: {productType: IProductType, amount: number}[];
    value: number;
    createdDate: Date;
    modifiedDate: Date;   
    
    isNotFinished: boolean;
}

export class Order implements IOrder {
    id: number;
    status: OrderStatus;
    products: { productType: IProductType; amount: number; }[];
    value: number;
    createdDate: Date;
    modifiedDate: Date;

    get isNotFinished(): boolean{
        console.log("isNotFinished has been called")
        return this.status != OrderStatus.finished
            && this.status != OrderStatus.cancelled
            && this.status != OrderStatus.cancelledByOrganisers;
    }
}
