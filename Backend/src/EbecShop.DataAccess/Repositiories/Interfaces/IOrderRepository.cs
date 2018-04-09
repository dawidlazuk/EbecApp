using EbecShop.DataAccess.Queries;
using EbecShop.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.DataAccess.Repositiories.Interfaces
{
    public interface IOrderRepository
    {
        Order Get(int id);
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetByQuery(OrderQuery query);

        Order Add(Order order, IDbConnection connection = null);
        Order Update(Order order, IDbConnection connection = null);

        Order GetFullOrder(int id);
        void Save(Order order, IDbConnection connection = null);
    }
}
