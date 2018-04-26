using EbecShop.Model.Enums;

namespace EbecShop.WebAPI.Shop.DTO
{
    public class OrderStateChange_Request
    {
        public int OrderId { get; set; }
        public OrderStatus State { get; set; }
    }
}
