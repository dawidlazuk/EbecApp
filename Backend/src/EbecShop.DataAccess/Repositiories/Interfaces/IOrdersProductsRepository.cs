using EbecShop.DataAccess.Helpers;
using EbecShop.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EbecShop.DataAccess.Repositiories.Interfaces
{
    public interface IOrdersProductsRepository
    {
        Task<IEnumerable<ProductAmount>> GetProductsOfOrder(int orderId);
    }
}
