using EbecShop.Model;
using EbecShop.Model.Enums;
using System.Collections.Generic;

namespace EbecShop.BizLogic.Contract
{
    public interface IShopLogic
    {
        Order CreateOrder(
            Team team,
            IDictionary<Product, decimal> products
            );

        Order ChangeOrderStatus(
            Order order,
            OrderStatus newStatus
            );
    }
}
