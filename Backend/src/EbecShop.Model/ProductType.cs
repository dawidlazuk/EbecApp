using EbecShop.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace EbecShop.Model
{
    public class ProductType : Entity
    {
        public Product Product { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}
