using EbecShop.Model.Abstract;
using System.Collections.Generic;

namespace EbecShop.Model
{
    public class Team : Entity
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public List<Participant> Members { get; set; }

        public Dictionary<Product,decimal> ProductLimits { get; set; }

        public Team()
        {
            Members = new List<Participant>();
            ProductLimits = new Dictionary<Product, decimal>();
        }      
    }
}