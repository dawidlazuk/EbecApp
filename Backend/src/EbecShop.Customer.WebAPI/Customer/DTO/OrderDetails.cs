using System.Collections.Generic;
using EbecShop.Model;

namespace EbecShop.WebAPI.Customer.DTO
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
