using EbecShop.DataAccess.Repositiories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbecShop.Model;
using Dapper;
using EbecShop.DataAccess.Repositiories.Abstract;

namespace EbecShop.DataAccess.Repositiories
{
    public class OrderRepository : Repository, IOrderRepository
    {
        public Order Find(int id)
        {
            return this.db.Query<Order>("SELECT * FROM Orders WHERE Id=@Id", new { Id = id }).FirstOrDefault();
        }

        public IEnumerable<Order> GetAll()
        {
            return this.db.Query<Order>("SELECT * FROM Orders");
        }
        
        public Order Add(Order order)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: order.Id, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.InputOutput);
            parameters.Add("@Status", order.Status);
            parameters.Add("@TeamId", order.TeamId);
            this.db.Execute("InsertOrder", parameters, commandType: System.Data.CommandType.StoredProcedure);
            order.Id = parameters.Get<int>("@Id");

            foreach(var product in order.Products)
            {
                var productParameter = new DynamicParameters();
                productParameter.Add("@OrderId", order.Id);
                productParameter.Add("@ProductId", product.Key.Id);
                productParameter.Add("@Amount", product.Value);
                this.db.Execute("InsertOrderProduct", parameters, commandType: System.Data.CommandType.StoredProcedure);                    
            }
            return order;
        }
        
        /// <summary>
        /// Method updates only simple fields of the order. Products in the order are not affected.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Order Update(Order order)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", order.Id);
            parameters.Add("@Status", order.Status);
            parameters.Add("@TeamId", order.TeamId);
            this.db.Execute("UpdateOrder", parameters, commandType: System.Data.CommandType.StoredProcedure);

            //TODO: Maybe update products in the order. To consider;

            return order;
        }

        public Order GetFullOrder(int id)
        {
            using (var multipleResults = this.db.QueryMultiple("GetOrder", new { Id = id }, commandType: System.Data.CommandType.StoredProcedure))
            {
                var order = multipleResults.Read<Order>().SingleOrDefault();
                var products = multipleResults.Read<Tuple<int, decimal>>().ToList();
                
                if(order != null && products != null)
                {
                    foreach(var productAmount in products)
                    {
                        order.Products.Add(new KeyValuePair<Product, decimal>(
                                DbContext.Products.Find(productAmount.Item1),
                                productAmount.Item2
                            ));
                    }
                }
                return order;
            }
        }

        public void Save(Order order)
        {
            if (order.IsNew)
                this.Add(order);
            else
                this.Update(order);        
        }

    }
}
