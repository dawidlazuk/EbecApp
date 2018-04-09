using EbecShop.DataAccess.Repositiories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbecShop.Model;
using Dapper;
using EbecShop.DataAccess.Repositiories.Abstract;
using EbecShop.DataAccess.Queries;
using System.Data;

namespace EbecShop.DataAccess.Repositiories
{
    public class OrderRepository : Repository, IOrderRepository
    {
        public Order Get(int id)
        {
            using(var connection = CreateDbConnection())
                return connection.Query<Order>("SELECT * FROM Orders WHERE Id=@Id", new { Id = id }).FirstOrDefault();
        }

        public IEnumerable<Order> GetAll()
        {
            using (var connection = CreateDbConnection())
                return connection.Query<Order>("SELECT * FROM Orders");
        }

        public IEnumerable<Order> GetByQuery(OrderQuery query)
        {
            using (var connection = CreateDbConnection())
                return query.ExecuteAsync(connection).Result;
        }
        
        public Order Add(Order order, IDbConnection connection = null)
        {
            //VS2017
            //using (var txScope = new TransactionScope())

            PerformOnDatabase(
                (conn) =>
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@Id", value: order.Id, dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.InputOutput);
                    parameters.Add("@Status", order.Status);
                    parameters.Add("@TeamId", order.TeamId);
                    conn.Execute("InsertOrder", parameters, commandType: System.Data.CommandType.StoredProcedure);
                    order.Id = parameters.Get<int>("@Id");

                    foreach (var product in order.Products)
                    {
                        var productParameters = new DynamicParameters();
                        productParameters.Add("@OrderId", order.Id);
                        productParameters.Add("@ProductTypeId", product.Key.Id);
                        productParameters.Add("@Amount", product.Value);
                        conn.Execute("InsertOrderProduct", productParameters, commandType: System.Data.CommandType.StoredProcedure);
                    }
                },
                connection);
            return order;
        }
        
        /// <summary>
        /// Method updates only simple fields of the order. Products in the order are not affected.
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public Order Update(Order order, IDbConnection connection = null)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@Id", order.Id);
            parameters.Add("@Status", order.Status);
            parameters.Add("@TeamId", order.TeamId);

            PerformOnDatabase(conn => connection.Execute("UpdateOrder", parameters, commandType: System.Data.CommandType.StoredProcedure),
                connection);

            //TODO: Maybe update products in the order. To consider;

            return order;
        }

        public Order GetFullOrder(int id)
        {
            using (var connection = CreateDbConnection())
            {
                using (var multipleResults = connection.QueryMultiple("GetOrder", new { Id = id }, commandType: System.Data.CommandType.StoredProcedure))
                {
                    var order = multipleResults.Read<Order>().SingleOrDefault();
                    var products = multipleResults.Read<Tuple<int, decimal>>().ToList();

                    if (order != null && products != null)
                    {
                        foreach (var productAmount in products)
                        {
                            order.Products.Add(new KeyValuePair<ProductType, decimal>(
                                    DbContext.ProductTypes.Find(productAmount.Item1),
                                    productAmount.Item2
                                ));
                        }
                    }
                    return order;
                }
            }
        }

        public void Save(Order order, IDbConnection connection = null)
        {
            if (order.IsNew)
                this.Add(order, connection);
            else
                this.Update(order, connection);        
        }
    }
}
