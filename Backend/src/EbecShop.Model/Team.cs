using EbecShop.Model.Abstract;
using System.Collections.Generic;

namespace EbecShop.Model
{
    public class Team : Entity
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }
        public decimal BlockedBalance { get; set; }
        
        public List<Participant> Members { get; set; }

        public Dictionary<ProductType,decimal> ProductLimits { get; set; }
        
        public Team()
        {
            Members = new List<Participant>();
            ProductLimits = new Dictionary<ProductType, decimal>();
        }      

        public decimal AvailableBalance
        {
            get { return Balance - BlockedBalance; }
        }
    }
}