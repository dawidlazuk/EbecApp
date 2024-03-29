import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import { IProduct, Product } from '../../shared/products/product';

@Injectable()
export class ProductsService {
  private _productCustomerUrl: string = 'http://localhost:49906/api/customer/products';

  constructor(private _http: HttpClient) {    
  }

  getProducts() : Observable<IProduct[]> 
  {
    return this._http.get<Product[]>(this._productCustomerUrl)
          .do(this.mapProducts)
          .catch(this.handleError);
  }

  private mapProducts(products: IProduct[])
  {    
    products.forEach(product => {
      product.id = Number(product.id);    
      product.types.forEach(type => {
        type.product = product;
        type.amount = Number(type.amount);
        type.price = Number(type.price);
        type.id = Number(type.id);
      });   
    }); 
  }

  private handleError(err: HttpErrorResponse){
    console.log(err.message);
    return Observable.throw(err.message);
  }
}
