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
        public ProductRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public Product Add(Product product)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: product.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@Name",product.Name);
            parameters.Add("@Description",product.Description);
            parameters.Add("@Image",product.Image);

            connection.Execute("InsertProduct", parameters, transaction: transaction, commandType: CommandType.StoredProcedure);

            product.Id = parameters.Get<int>("@Id");
            return product;
        }

        public Product Find(int id)
        {
            return connection.Query<Product>("SELECT * FROM Products WHERE Id = @Id", new { Id = id }, transaction: transaction).FirstOrDefault();
        }

        public Product Update(Product product)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: product.Id);
            parameters.Add("@Name", product.Name);
            parameters.Add("@Description", product.Description);
            parameters.Add("@Image", product.Image);

            connection.Execute("UpdateProduct", parameters, transaction: transaction, commandType: CommandType.StoredProcedure);

            return product;
        }

        public IEnumerable<Product> GetAll()
        {
            var products = connection.Query<Product>("SELECT * FROM Products", transaction: transaction);

            foreach (var product in products)
            {
                product.Types = connection.Query<ProductType>("SELECT * FROM ProductTypes WHERE ProductId=@Id", new { Id = product.Id }, transaction: transaction);
                foreach (var type in product.Types)
                    type.Product = product;
            }
            return products;
        }

        public Product GetFullProduct(int id)
        {
            using (var multipleResults = connection.QueryMultiple("GetProduct", new { Id = id }, transaction: transaction, commandType: CommandType.StoredProcedure))
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
