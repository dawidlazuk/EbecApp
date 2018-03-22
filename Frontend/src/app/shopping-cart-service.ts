import { Injectable } from "@angular/core";
import { IProductType } from "./product/productType";

@Injectable()
export class ShoppingCartService {
    customer: {
        balance: number;
     } //TODO change for customer (team) type
    products: {[productTypeId: number] : number; } = {};

     constructor() {
         this.customer = {
            balance: 1000
         }         
     }

    public addProduct(product: IProductType, amount: number): Boolean {
        //TODO verify is the customer is able to order that amount
        
        if(product.amount < amount)
        {
            console.log('Product has not been added, not enough amount');
            return false;
        }

        if(this.customer.balance <= amount*product.price)
        {
            console.log('Product has not been added, not enough balance');
            return false;
        }

        if(this.products[product.id] === undefined)
            this.products[product.id] = amount;
        else
            this.products[product.id] += amount;

        product.amount -= amount;

        console.log('Product ' + product.name + ' has been added to cart. Amount: ' + amount);
        return true;
    }
}
