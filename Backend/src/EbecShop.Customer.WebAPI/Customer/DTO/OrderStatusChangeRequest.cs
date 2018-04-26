using EbecShop.Model.Enums;

namespace EbecShop.WebAPI.Customer.DTO
{
    public class OrderStatusChangeRequest
    {
        public OrderStatus Status { get; set; }
    }
}
