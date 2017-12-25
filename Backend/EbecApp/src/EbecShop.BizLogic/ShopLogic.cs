using System.Collections.Generic;
using EbecShop.Model;
using EbecShop.DataAccess;
using EbecShop.BizLogic.Contract;
using EbecShop.Model.Enums;
using System;

namespace EbecShop.BizLogic
{
    class ShopLogic : IShopLogic
    {
        public Order CreateOrder(Team team, IDictionary<Product, decimal> products)
        {
            var order = new Order()
            {
                Team = team,
                Status = Model.Enums.OrderStatus.New
            };
                       
            foreach(var product in products)
            {
                //TODO check amount of the products in store

                var limit = DbContext.Teams.GetProductLimitForTeam(team, product.Key);
                if (product.Value > limit)
                    throw new ExceededLimitException() { Team = team, Product = product.Key, OrderedAmount = product.Value, Limit = limit };
            }

            order = DbContext.Orders.Add(order);
            return order;
        }

        public Order ChangeOrderStatus(Order order, OrderStatus newStatus)
        {
            CheckIfNewOrderStatusIsValid(order.Status, newStatus);

            order.Status = newStatus;
            order = DbContext.Orders.Update(order);
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
    }
}
