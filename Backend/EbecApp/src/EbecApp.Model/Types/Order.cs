using EbecApp.Model.Types.Abstract;
using EbecApp.Model.Types.Enums;
using System.Collections.Generic;

namespace EbecApp.Model.Types
{

    public class Order : Entity
    {
        public Team TeamId { get; set; }

        public OrderStatus Status { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
