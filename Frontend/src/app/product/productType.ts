export interface IProductType{
    name: string;
    quantityAvailable: number;   
}

export class ProductType implements IProductType {
    constructor(public name: string = "Typ",                
                public quantityAvailable: number = 0)
    {
    }
}