using EbecShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.DataAccess.Repositiories.Interfaces
{
    public interface IProductRepository
    {
        Product Get(int id);
        IEnumerable<Product> GetAll();

        Product Add(Product product);
        Product Update(Product product);

        Product GetFullProduct(int id);
        void Save(Product product);
    }
}
