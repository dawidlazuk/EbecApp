export interface IProduct{
    name: string;
    description: string;
    imageUrl: string;
    types: any[];    
}

export class Product implements IProduct {

    constructor(public name: string,
                public description: string,
                public imageUrl: string,
                public types: any[])
    {
    }
}