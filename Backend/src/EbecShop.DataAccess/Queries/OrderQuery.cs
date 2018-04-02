using Dapper;
using EbecShop.Model;
using EbecShop.Model.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EbecShop.DataAccess.Queries
{
    public class OrderQuery : Query<Order>
    {
        public int? OrderId { get; set; }
        public int? TeamId { get; set; }
        public OrderStatus? Status { get; set; }
        

        public override string GetSqlQuery()
        {
            IsFirstContitionAppended = false;
            var builder = new StringBuilder();
            builder.Append("SELECT * FROM Orders ");

            if (Status != null)
            {
                builder.Append($"{GetSqlKeyword()} Status = @Status ");
                IsFirstContitionAppended = true;
            }

            if (TeamId != null)
                builder.Append($"{GetSqlKeyword()} TeamId = @TeamId");

            return builder.ToString();
        }

        private class ProductAmount
        {
            internal int ProductTypeId { get; set; }
            internal decimal Amount { get; set; }
        }

        internal override async System.Threading.Tasks.Task<IEnumerable<Order>> ExecuteAsync(IDbConnection connection)
        {
            DynamicParameters parameters = CreateParameters();

            var orders = await connection.QueryAsync<Order>("GetOrderByQuery", parameters, commandType: CommandType.StoredProcedure);

            foreach (var order in orders)
            {
                var productIdsAndAmounts = await connection.QueryAsync<ProductAmount>("GetProductsOfOrder", new { OrderId = order.Id }, commandType: CommandType.StoredProcedure);
                order.Products = new Dictionary<ProductType, decimal>();
                foreach (var productAmount in productIdsAndAmounts)
                    order.Products.Add(DbContext.ProductTypes.GetProductType(productAmount.ProductTypeId), productAmount.Amount);

            }
            return orders;
        }

        private DynamicParameters CreateParameters()
        {
            var parameters = new DynamicParameters();
            if (OrderId.HasValue)
                parameters.Add("@Id", OrderId);

            if (Status.HasValue)
                parameters.Add("@Status", Status);

            if (TeamId.HasValue)
                parameters.Add("@TeamId", TeamId);
            return parameters;
        }
    }
}