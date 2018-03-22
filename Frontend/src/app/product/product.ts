import { ProductType } from "./productType";

export interface IProduct{
    name: string;
    description: string;
    imageUrl: string;
    types: ProductType[];    
}

export class Product implements IProduct {

    constructor(public name: string,
                public description: string,
                public imageUrl: string,
                public types: ProductType[] = [new ProductType(null)])
    {
    }
}