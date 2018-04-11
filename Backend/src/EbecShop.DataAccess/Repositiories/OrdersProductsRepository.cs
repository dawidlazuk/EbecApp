using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using EbecShop.DataAccess.Helpers;
using EbecShop.DataAccess.Repositiories.Abstract;
using EbecShop.DataAccess.Repositiories.Interfaces;
using EbecShop.Model;

namespace EbecShop.DataAccess.Repositiories
{
    class OrdersProductsRepository : Repository, IOrdersProductsRepository
    {
        public OrdersProductsRepository(IDbTransaction transaction) 
            : base(transaction)
        {
        }

        public async Task<IEnumerable<ProductAmount>> GetProductsOfOrder(int orderId)
        {
            return await connection.QueryAsync<ProductAmount>(
                "GetProductsOfOrder",
                new { OrderId = orderId },
                transaction: transaction,
                commandType: CommandType.StoredProcedure);
        }
    }
}
