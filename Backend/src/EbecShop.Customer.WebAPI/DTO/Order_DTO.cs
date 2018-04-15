using EbecShop.Model.Enums;
using System;

namespace EbecShop.Customer.WebAPI.DTO
{
    public class Order_DTO
    {
        public int Id { get; set; }
        public Team_DTO Team { get; set; }
        public OrderStatus Status {get;set;}
        public string Comment { get; set; }
        public decimal Value { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }


        public static Order_DTO MapFromModel(EbecShop.Model.Order order)
        {
            return new Order_DTO
            {
                Id = order.Id,
                Team = Team_DTO.MapFromModel(order.Team),                
                Status = order.Status,
                Comment = order.Comment,
                Value = order.Value,
                CreatedDate = default(DateTime),
                ModifiedDate = default(DateTime)
            };
        }
    }
}
