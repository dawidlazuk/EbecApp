using EbecApp.Model.Abstract;
using EbecApp.Model.Enums;
using System.Collections.Generic;

namespace EbecApp.Model
{

    public class Order : Entity
    {
        public Team TeamId { get; set; }

        public OrderStatus Status { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
