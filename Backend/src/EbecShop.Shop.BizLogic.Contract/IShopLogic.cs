using EbecShop.Model;
using EbecShop.Model.Enums;
using System;
using System.Threading.Tasks;

namespace EbecShop.Shop.BizLogic.Contract
{
    public interface IShopLogic
    {
        [Obsolete]
        Order ChangeOrderStatus(
            Order order,
            OrderStatus newStatus
            );

        Order SetOrderState_InProgress(int orderId);
        Task<Order> SetOrderState_InProgress_Async(int orderId);

        Order SetOrderState_ReadyToReceive(int orderId);
        Task<Order> SetOrderState_ReadyToReceive_Async(int orderId);

        Order SetOrderState_Finished(int orderId);
        Task<Order> SetOrderState_Finished_Async(int orderId);

        Team CreateNewTeam(string name, decimal balance);
    }
}
