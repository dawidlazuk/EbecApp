using System.Collections.Generic;
using EbecShop.Model;
using EbecShop.DataAccess;
using EbecShop.Shop.BizLogic.Contract;
using EbecShop.Model.Enums;
using System;

namespace EbecShop.Shop.BizLogic
{
    class ShopLogic : IShopLogic
    {
        #region Orders


        public Order ChangeOrderStatus(Order order, OrderStatus newStatus)
        {
            CheckIfNewOrderStatusIsValid(order.Status, newStatus);

            order.Status = newStatus;
            using(var unitOfWork = new UnitOfWork())
                order = unitOfWork.Orders.Update(order);
            return order;
        }

        private static void CheckIfNewOrderStatusIsValid(OrderStatus previousStatus, OrderStatus newStatus)
        {
            switch (newStatus)
            {
                case OrderStatus.New:
                    throw new ArgumentException("Set order status to New is not allowed.");

                case OrderStatus.InProgress:
                    if (previousStatus != OrderStatus.New)
                        throw new ArgumentException($"Only New orders are allowed to be set into InProgress. Actual status is {previousStatus}.");
                    break;

                case OrderStatus.ReadyToReceive:
                    if (previousStatus != OrderStatus.InProgress)
                        throw new ArgumentException($"Only InProgress orders are allowed to be set into ReadyToReceive. Actual status is {previousStatus}.");
                    break;

                case OrderStatus.Finished:
                    if (previousStatus != OrderStatus.ReadyToReceive)
                        throw new ArgumentException($"Only ReadyToReceive orders are allowed to be set into Finished. Actual status is {previousStatus}.");
                    break;

                case OrderStatus.Cancelled:
                case OrderStatus.CancelledByOrganisers:
                    if (previousStatus == OrderStatus.Finished)
                        throw new ArgumentException("Finished orders can not be cancelled.");
                    break;
            }
        }

        #endregion


    }
}
