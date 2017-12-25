using EbecShop.Model;
using EbecShop.Model.Enums;
using System.Collections.Generic;

namespace EbecShop.Shop.BizLogic.Contract
{
    public interface IShopLogic
    {
        Order ChangeOrderStatus(
            Order order,
            OrderStatus newStatus
            );
    }
}
