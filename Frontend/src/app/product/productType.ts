import { IProduct } from "./product";

export interface IProductType{
    id: number;
    name: string;
    amount: number;  
    price: number;
 
    product: IProduct;
}

export class ProductType implements IProductType {
    id: number;
    constructor(public product: IProduct,
                public name: string = "Typ",                
                public amount: number = 0,
                public price: number = 0)
    {
    }
}