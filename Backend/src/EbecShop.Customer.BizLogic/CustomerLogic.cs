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

        public Order CreateOrder(int teamId, IDictionary<int, decimal> productsIds)
        {
            Team team = DbContext.Teams.GetTeam(teamId);
            Debug.Assert(team != null);

            var products = new Dictionary<ProductType, decimal>();
            foreach(var productAmount in productsIds)
            {
                var product = DbContext.ProductTypes.GetProductType(productAmount.Key);
                Debug.Assert(product != null);
                products.Add(product, productAmount.Value);
            }
            return CreateOrder(team, products);
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
                var orderValue = products.Sum(pa => pa.Key.Price * pa.Value);
                if (orderValue < team.AvailableBalance)
                {
                    DbContext.ExecuteAsTransaction(() =>
                    {
                        team.BlockedBalance += orderValue;
                        order = DbContext.Orders.Add(order);
                        team = DbContext.Teams.Update(team);
                    });
                }
                else
                {
                    throw new ArgumentException("Team has not enough funds to create order.");
                }
            }
            return order;
        }

        private static void CheckProductsLimits(Team team, Order order, IDictionary<ProductType, decimal> products)
        {
            var productsToRemove = new List<ProductType>();
            foreach (var product in products)
            {
                var amountInStore = DbContext.ProductTypes.Find(product.Key.Id).Amount;
                if (product.Value > amountInStore)
                {
                    productsToRemove.Add(product.Key);
                    order.Comment += GetLinePrefix(order.Comment) + $"Product {product.Key.Name} removed due to not enough amount in store. Ordered: {product.Value}, available: {amountInStore}.";
                    continue;
                }

                var limit = DbContext.Teams.GetProductLimitForTeam(team, product.Key);
                if (product.Value > limit)
                {
                    productsToRemove.Add(product.Key);
                    order.Comment += GetLinePrefix(order.Comment) + $"Product {product.Key.Name} removed due to exceeded limit per team. Ordered: {product.Value}, allowed: {limit}.";
                }
            }

            foreach (var product in productsToRemove)
                products.Remove(product);
        }

        private static string GetLinePrefix(string previousString)
        {
            return string.IsNullOrEmpty(previousString) ? "" : "\n";
        }


        public Order CancelOrder(Order order)
        {
            if (order.Status == OrderStatus.Finished)
                throw new ArgumentException("Finished orders can not be cancelled.");

            order.Status = OrderStatus.Cancelled;
            DbContext.Orders.Save(order);
            CustomerLogicObserver.OnOrderCancelled(order.Id);
            return order;
        }

        public IEnumerable<Order> GetTeamOrders(Team team)
        {

            return DbContext.Orders.GetByQuery(new OrderQuery { TeamId = team.Id });
        }

        #endregion

        #region Products

        public IEnumerable<Product> GetProducts()
        {
            return DbContext.Products.GetAll();
        }

        public Product GetProduct(int id)
        {
            return DbContext.Products.GetFullProduct(id);
        }


        #endregion

    }
}
