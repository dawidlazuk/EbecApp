using EbecShop.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.Customer.WebAPI.DTO
{
    public class Order
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public OrderStatus Status {get;set;}
        public string Comment { get; set; }
        public decimal Value { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }


        public static Order MapFromModel(EbecShop.Model.Order order)
        {
            return new Order
            {
                Id = order.Id,
                TeamId = order.TeamId,
                Status = order.Status,
                Comment = order.Comment,
                Value = order.Value,
                CreatedDate = default(DateTime),
                ModifiedDate = default(DateTime)
            };
        }
    }
}
