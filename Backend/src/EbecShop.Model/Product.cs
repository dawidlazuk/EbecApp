using EbecShop.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.Model
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}
