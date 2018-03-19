using EbecShop.DataAccess.Repositiories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbecShop.Model;
using EbecShop.DataAccess.Repositiories.Abstract;
using System.Data;
using Dapper;

namespace EbecShop.DataAccess.Repositiories
{
    public class ProductRepository : Repository, IProductRepository
    {
        public Product Add(Product product)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: product.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@Name",product.Name);
            parameters.Add("@Description",product.Description);
            parameters.Add("@Image",product.Image);
            //parameters.Add("@Price",product.Price);
            //parameters.Add("@Amount", product.Amount);
            this.db.Execute("InsertProduct", parameters, commandType: CommandType.StoredProcedure);
            product.Id = parameters.Get<int>("@Id");

            return product;
        }

        public Product Find(int id)
        {
            return this.db.Query<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = id }).FirstOrDefault();
        }

        public Product Update(Product product)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: product.Id);
            parameters.Add("@Name", product.Name);
            parameters.Add("@Description", product.Description);
            parameters.Add("@Image", product.Image);
            //parameters.Add("@Price", product.Price);
            //parameters.Add("@Amount", product.Amount);
            this.db.Execute("UpdateProduct", parameters, commandType: CommandType.StoredProcedure);
            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            var products = this.db.Query<Product>("SELECT * FROM Products");

            foreach (var product in products)
                product.Types = this.db.Query<ProductType>("SELECT * FROM ProductTypes WHERE ProductId=@Id", new { Id = product.Id });

            return products;
        }

        public Product GetFullProduct(int id)
        {
            using (var multipleResults = this.db.QueryMultiple("GetProduct", new { Id = id }, commandType: CommandType.StoredProcedure))
            {
                var product = multipleResults.Read<Product>().Single();
                var productTypes = multipleResults.Read<ProductType>().ToList();

                if (product != null && productTypes != null)
                {
                    productTypes.ForEach(type => type.Product = product);
                    product.Types = productTypes;
                }
                return product;
            }
        }

        public void Save(Product product)
        {
            if (product.IsNew)
                this.Add(product);
            else
                this.Update(product);
        }

    }
}
