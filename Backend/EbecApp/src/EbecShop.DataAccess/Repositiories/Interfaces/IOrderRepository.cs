using EbecShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.DataAccess.Repositiories.Interfaces
{
    public interface IOrderRepository
    {
        Order Find(int id);
        IEnumerable<Order> GetAll();

        Order Add(Order order);
        Order Update(Order order);

        Order GetFullOrder(int id);
        void Save(Order order);
    }
}
