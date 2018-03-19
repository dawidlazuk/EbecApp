export interface IProductType{
    name: string;
    amount: number;   
}

export class ProductType implements IProductType {
    constructor(public name: string = "Typ",                
                public amount: number = 0)
    {
    }
}