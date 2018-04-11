using EbecShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.Customer.WebAPI.DTO
{
    public class ProductTypeWithAmount
    {
        public ProductType ProductType { get; set; }
        public decimal Amount { get; set; }
    }

    public class OrderDetails
    {        
        public List<ProductTypeWithAmount> Products { get; set; }

        public static OrderDetails MapFromModel(EbecShop.Model.Order order)
        {
            var products = new List<ProductTypeWithAmount>();
            foreach(var product in order.Products)            
                products.Add(new ProductTypeWithAmount { ProductType = product.Key, Amount = product.Value });            

            return new OrderDetails { Products = products };
        }
    }
}
