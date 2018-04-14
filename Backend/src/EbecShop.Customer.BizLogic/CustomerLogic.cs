using EbecShop.Customer.BizLogic.Contract;
using EbecShop.DataAccess;
using EbecShop.Model;
using System.Collections.Generic;
using System;
using EbecShop.Model.Enums;
using EbecShop.DataAccess.Queries;
using System.Linq;
using System.Transactions;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EbecShop.Customer.BizLogic
{
    public class CustomerLogic : ICustomerLogic
    {
        private ICustomerLogicObserver _observer;
        public ICustomerLogicObserver CustomerLogicObserver
        {
            get { return _observer ?? (_observer = new DefaultCustomerLogicObserver()); }
            set { _observer = value; }
        }

        #region Orders

        public Order GetOrder(int id)
        {
            return DbAccessLayer.Orders.GetOrdersByQueryAsync(new OrderQuery { OrderId = id }).Result.Single();            
        }

        public Order CreateOrder(int teamId, IDictionary<int, decimal> productsIds)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                Team team = unitOfWork.Teams.GetTeam(teamId);
                Debug.Assert(team != null);

                var products = new Dictionary<ProductType, decimal>();
                foreach (var productAmount in productsIds)
                {
                    var product = unitOfWork.ProductTypes.GetProductType(productAmount.Key);
                    Debug.Assert(product != null);
                    products.Add(product, productAmount.Value);
                }
                return CreateOrder(team, products);
            }
        }

        public Order CreateOrder(Team team, IDictionary<ProductType, decimal> products)
        {
            var order = new Order()
            {
                Team = team,
                Status = OrderStatus.New
            };

            CheckProductsLimits(team, order, products);

            order.Products = products;
            if (order.Products.Any())
            {
                if (order.Value < team.AvailableBalance)
                    DbAccessLayer.Orders.AddNewOrder(team, order);
                else
                    throw new ArgumentException("Team has not enough funds to create order.");
            }
            return order;
        }

        private static void CheckProductsLimits(Team team, Order order, IDictionary<ProductType, decimal> products)
        {
            var productsToRemove = new List<ProductType>();

            using (var unitOfWork = new UnitOfWork())
            {
                foreach (var product in products)
                {
                    var amountInStore = unitOfWork.ProductTypes.Get(product.Key.Id).Amount;
                    if (product.Value > amountInStore)
                    {
                        productsToRemove.Add(product.Key);
                        order.Comment += GetLinePrefix(order.Comment) + $"Product {product.Key.Name} removed due to not enough amount in store. Ordered: {product.Value}, available: {amountInStore}.";
                        continue;
                    }

                    var limit = unitOfWork.Teams.GetProductLimitForTeam(team, product.Key);
                    if (product.Value > limit)
                    {
                        productsToRemove.Add(product.Key);
                        order.Comment += GetLinePrefix(order.Comment) + $"Product {product.Key.Name} removed due to exceeded limit per team. Ordered: {product.Value}, allowed: {limit}.";
                    }
                }
            }

            foreach (var product in productsToRemove)
                products.Remove(product);
        }

        private static string GetLinePrefix(string previousString)
        {
            return string.IsNullOrEmpty(previousString) ? "" : "\n";
        }


        public async Task<Order> CancelOrder(int id)
        {
            Order order = (await DbAccessLayer.Orders.GetOrdersByQueryAsync(new OrderQuery { OrderId = id })).Single();
            return CancelOrder(order);
        }

        public Order CancelOrder(Order order)
        {
            if (order.Status == OrderStatus.Finished || order.Status == OrderStatus.Cancelled)
                throw new ArgumentException("Finished or cancelled orders can not be cancelled.");
            
            using (var unitOfWork = new UnitOfWork())
            {
                //TODO consider if querying the database for the modified data is necessary. Maybe we could rely on data contained in the object already.
                order.Status = OrderStatus.Cancelled;
                unitOfWork.Orders.Save(order);

                foreach (var productAmount in order.Products)
                {
                    var productType = unitOfWork.ProductTypes.Get(productAmount.Key.Id);
                    productType.Amount += productAmount.Value;
                    unitOfWork.ProductTypes.Save(productType);
                }

                var team = unitOfWork.Teams.Get(order.TeamId);
                team.BlockedBalance -= order.Value;
                unitOfWork.Teams.Save(team);

                unitOfWork.Commit();
            }
            CustomerLogicObserver.OnOrderCancelled(order.Id);
            return order;
        }

        public async Task<IEnumerable<Order>> GetTeamOrders(Team team)
        {
            return await DbAccessLayer.Orders.GetOrdersByQueryAsync(new OrderQuery { TeamId = team.Id });
        }

        #endregion

        #region Products

        public IEnumerable<Product> GetProducts()
        {
            using(var unitOfWork = new UnitOfWork())
                return unitOfWork.Products.GetAll();
        }

        public Product GetProduct(int id)
        {
            using (var unitOfWork = new UnitOfWork())
                return unitOfWork.Products.GetFullProduct(id);
        }

       
        #endregion

    }
}
