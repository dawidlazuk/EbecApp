import { Injectable } from "@angular/core";
import { IProductType, ProductType } from "../product/productType";
import {BehaviorSubject} from 'rxjs/BehaviorSubject';

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
        
        console.log(productType.id)      ;
        var productAlreadyInCart = this.products.filter(entry => ProductType.AreEqual(entry.productType, productType))[0];

       
        if(productAlreadyInCart != undefined)
        {
            console.log(productAlreadyInCart.productType.id);
            productAlreadyInCart.amount = Number(productAlreadyInCart.amount) + Number(amount);
        }
        else
        {
            this.products.push({productType, amount});
        }
        productType.amount -= amount;

        // if(this.products.indexOf(product) < 0)
        //     this.products.concat([product]);        
        // this.amounts[product.id] += amount;
        
        this._onCartChangedSource.next(this.products);

        console.log('Product ' + productType.name + ' has been added to cart. Amount: ' + amount);
        console.log('Cart',this.products);
        return true;
    }

    
}
