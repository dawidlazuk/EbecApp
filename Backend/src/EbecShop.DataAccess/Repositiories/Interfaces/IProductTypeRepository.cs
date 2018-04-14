using System.Collections.Generic;
using EbecShop.Model;

namespace EbecShop.DataAccess.Repositiories.Interfaces
{
    public interface IProductTypeRepository
    {
        ProductType Get(int id);
        IEnumerable<ProductType> GetAll();

        ProductType Add(ProductType productType);
        ProductType Update(ProductType productType);

        ProductType GetProductType(int id);
        void Save(ProductType productType);
    }
}
