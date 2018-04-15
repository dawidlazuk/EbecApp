import { Product } from "./product";

export interface IProductType{
    id: number;
    name: string;
    amount: number;  
    price: number;
 
    product: Product;
}

export class ProductType implements IProductType {     
    constructor(public product: Product,
                public name: string = "Typ",                
                public amount: number = 0,
                public price: number = 0,
                public id: number = 0)
    {
    }

    static AreEqual(a: IProductType, b: IProductType): boolean {   
        return a.product.id == b.product.id
            && a.id == b.id;
    }
}