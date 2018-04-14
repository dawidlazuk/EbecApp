using EbecShop.DataAccess.Queries;
using EbecShop.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EbecShop.DataAccess
{
    public static class DbAccessLayer
    {
        public static class Orders
        {
            public static void AddNewOrder(Team team, Order order)
            {
                using (var unitOfWork = new UnitOfWork(ConfigurationReader.ConnectionString))
                {
                    team = unitOfWork.Teams.GetTeam(team.Id);

                    if (team.AvailableBalance < order.Value)
                        throw new ArgumentException($"Team {team.Name} [{team.Id}] has not enough available balance. Available: {team.AvailableBalance}. Order value: {order.Value}.");

                    var productTypesIdsToRemove = new List<int>();

                    //check and update availability of product
                    foreach(var productAmount in order.Products)
                    {
                        var productType = unitOfWork.ProductTypes.Get(productAmount.Key.Id);
                        if (productType.Amount >= productAmount.Value)
                        {
                            productType.Amount -= productAmount.Value;
                            unitOfWork.ProductTypes.Save(productType);
                        }
                        else
                        {
                            productTypesIdsToRemove.Add(productType.Id);
                            order.AddComment($"Product {productType.Product.Name} ({productType.Name}) has been removed because of not enough amount in stock.");
                        }
                    }

                    team.BlockedBalance += order.Value;
                    order = unitOfWork.Orders.Add(order);
                    team = unitOfWork.Teams.Update(team);

                    unitOfWork.Commit();
                }
            }


            public static async Task<IEnumerable<Order>> GetOrdersByQuery(OrderQuery query)
            {
                using (var unitOfWork = new UnitOfWork(ConfigurationReader.ConnectionString))
                {
                    var orders = await unitOfWork.Orders.GetByQuery(query);
                    foreach (var order in orders)
                    {
                        var productIdsAndAmounts = await unitOfWork.OrdersProducts.GetProductsOfOrder(order.Id);
                        order.Products = new Dictionary<ProductType, decimal>();
                        foreach (var productAmount in productIdsAndAmounts)
                            order.Products.Add(unitOfWork.ProductTypes.GetProductType(productAmount.ProductTypeId), productAmount.Amount);
                    }
                    return orders;
                }
            }
        }

        public static class Participants
        {
            public static Participant GetParticipant(int id)
            {
                using(var unitOfWork = new UnitOfWork())
                {
                    var participant = unitOfWork.Participants.Find(id);
                    if (participant == null)
                        throw new KeyNotFoundException($"Participant with id {id} does not exist.");

                    participant.Team = unitOfWork.Teams.Get(participant.TeamId);

                    return participant;
                }
            }
        }        
    }
}
