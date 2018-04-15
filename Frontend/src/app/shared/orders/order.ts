import { OrderStatus } from "./order-status.enum";
import { IProductType } from "../products/productType";
import { Team } from "../teams/team";

export interface IOrder{
    id: number;
    team: Team;
    status: OrderStatus;
    products: {productType: IProductType, amount: number}[];
    value: number;
    createdDate: Date;
    modifiedDate: Date;   
    
    isNotFinished: boolean;
}

export class Order implements IOrder {
    id: number;
    team: Team;
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
