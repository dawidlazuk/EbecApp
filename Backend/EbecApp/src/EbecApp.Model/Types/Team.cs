using EbecApp.Model.Types.Abstract;
using System.Collections.Generic;

namespace EbecApp.Model.Types
{
    public class Team : Entity
    {
        public string Name { get; set; }

        public decimal Balance { get; set; }

        public IEnumerable<Participant> Members { get; set; }

        public IDictionary<Product,decimal> ProductLimits { get; set; }
    }
}