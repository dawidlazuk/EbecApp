using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using EbecShop.DataAccess.Repositiories.Abstract;
using EbecShop.DataAccess.Repositiories.Interfaces;
using EbecShop.Model;

namespace EbecShop.DataAccess.Repositiories
{
    class ProductTypeRepository : Repository, IProductTypeRepository
    {
        public ProductType Add(ProductType productType)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: productType.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@ProductId", productType.Product.Id);
            parameters.Add("@Name", productType.Name);
            parameters.Add("@Price", productType.Price);
            parameters.Add("@Amount", productType.Amount);
            this.db.Execute("InsertProductType", parameters, commandType: CommandType.StoredProcedure);
            productType.Id = parameters.Get<int>("@Id");

            return productType;
        }

        public ProductType Find(int id)
        {
            return this.db.Query<ProductType>("SELECT * FROM ProductTypes WHERE Id = @Id", new { Id = id }).FirstOrDefault();
        }

        public IEnumerable<ProductType> GetAll()
        {
            return this.db.Query<ProductType>("SELECT * FROM ProductTypes");
        }

        public ProductType GetFullProductType(int id)
        {
            ProductType productType;
            using(var multipleResults = this.db.QueryMultiple("GetProductType", new { Id = id }, commandType: CommandType.StoredProcedure))
            {
                productType = multipleResults.Read<ProductType>().Single();
                var product = multipleResults.Read<Product>().Single();
                if (productType != null)
                    productType.Product = product;
            }
            return productType;
        }

        public void Save(ProductType productType)
        {
            if (productType.IsNew)
                this.Add(productType);
            else
                this.Update(productType);
        }

        public ProductType Update(ProductType productType)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: productType.Id);
            parameters.Add("@ProductId", productType.Product.Id);
            parameters.Add("@Name", productType.Name);
            parameters.Add("@Price", productType.Price);
            parameters.Add("@Amount", productType.Amount);
            this.db.Execute("UpdateProductType", parameters, commandType: CommandType.StoredProcedure);
           
            return productType;
        }
    }
}
