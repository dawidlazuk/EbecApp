import { Injectable } from "@angular/core";
import { IProductType, ProductType } from "../product/productType";
import {BehaviorSubject} from 'rxjs/BehaviorSubject';
import { Product } from "../product/product";

@Injectable()
export class ShoppingCartService {
    customer: {
        balance: number;
     } //TODO change for customer (team) type
    //products: IProductType[] = [];
    // amounts: {[productTypeId: number] : number} = [];

    products: {productType: IProductType, amount: number}[] = [];

    private _onCartChangedSource = new BehaviorSubject<{productType: IProductType, amount: number}[]>(null);
    onCartChanged$ = this._onCartChangedSource.asObservable();

    constructor() {
        this.customer = {
           balance: 1000
        };
    }

    public addProduct(productType: IProductType, amount: number): Boolean {
        //TODO verify is the customer is able to order that amount
        amount = Number(amount);
        if (amount <= 0)
            return;

        if(productType.amount < amount)
        {
            console.log('Product has not been added, not enough amount');
            return false;
        }

        if(this.customer.balance <= amount*productType.price)
        {
            console.log('Product has not been added, not enough balance');
            return false;
        }
        
        var productAlreadyInCart = this.products.filter(entry => ProductType.AreEqual(entry.productType, productType))[0];  

        if(productAlreadyInCart != undefined)        
            productAlreadyInCart.amount = productAlreadyInCart.amount + amount;        
        else
            this.products.push({productType, amount});        
        productType.amount -= amount;

        this._onCartChangedSource.next(this.products);
        return true;
    }

    public removeProduct(productType: IProductType): void{
        var index = this.products.findIndex(entry => ProductType.AreEqual(entry.productType, productType));
        if(index > -1){
            this.products.splice(index, 1);
            this._onCartChangedSource.next(this.products);
        }
    }

    public clear(): void{
        this.products = [];
        this._onCartChangedSource.next(this.products);
    }
}
