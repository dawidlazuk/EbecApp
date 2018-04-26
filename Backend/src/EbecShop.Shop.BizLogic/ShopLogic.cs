using System.Collections.Generic;
using EbecShop.Model;
using EbecShop.DataAccess;
using EbecShop.Shop.BizLogic.Contract;
using EbecShop.Model.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;
using EbecShop.DataAccess.Queries;

namespace EbecShop.Shop.BizLogic
{
    public class ShopLogic : IShopLogic
    {
        #region Orders


        public Order ChangeOrderStatus(Order order, OrderStatus newStatus)
        {
            CheckIfNewOrderStatusIsValid(order.Status, newStatus);

            order.Status = newStatus;
            using (var unitOfWork = new UnitOfWork())
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

        public Order SetOrderState_InProgress(int orderId)
        {
            return SetOrderState_InProgress_Async(orderId).Result;
        }

        public async Task<Order> SetOrderState_InProgress_Async(int orderId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var order = (await DbAccessLayer.Orders.GetOrdersByQueryAsync(new OrderQuery { OrderId = orderId })).Single();
                if (order.Status != OrderStatus.New)
                    throw new ArgumentException("Order status is invalid. New order required.");

                order.Status = OrderStatus.InProgress;
                order = unitOfWork.Orders.Update(order);

                unitOfWork.Commit();
                return order;
            }
        }

        public Order SetOrderState_ReadyToReceive(int orderId)
        {
            return SetOrderState_ReadyToReceive_Async(orderId).Result;
        }

        public async Task<Order> SetOrderState_ReadyToReceive_Async(int orderId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var order = (await DbAccessLayer.Orders.GetOrdersByQueryAsync(new OrderQuery { OrderId = orderId })).Single();
                if (order.Status != OrderStatus.InProgress)
                    throw new ArgumentException("Order status is invalid. In progress state required.");

                order.Status = OrderStatus.ReadyToReceive;
                order = unitOfWork.Orders.Update(order);

                unitOfWork.Commit();
                return order;
            }
        }

        public Order SetOrderState_Finished(int orderId)
        {
            return SetOrderState_Finished_Async(orderId).Result;
        }

        public async Task<Order> SetOrderState_Finished_Async(int orderId)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                var order = (await DbAccessLayer.Orders.GetOrdersByQueryAsync(new OrderQuery { OrderId = orderId })).Single();
                if (order.Status != OrderStatus.ReadyToReceive)
                    throw new ArgumentException("Order status is invalid. Ready to receive state required.");
                
                if (order.TeamId <= 0)
                    throw new ArgumentException("Order does not have TeamId provided.");

                var team = unitOfWork.Teams.Get(order.TeamId);
                team.BlockedBalance -= order.Value;
                team.Balance -= order.Value;
                unitOfWork.Teams.Update(team);

                order.Status = OrderStatus.Finished;
                order.Team = team;
                order = unitOfWork.Orders.Update(order);

                unitOfWork.Commit();
                return order;
            }
        }

        #endregion
    }
}
