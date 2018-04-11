using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.DataAccess.Repositiories.Abstract
{
    public abstract class Repository
    {
        protected IDbTransaction transaction;
        protected IDbConnection connection => transaction.Connection;

        public Repository(IDbTransaction transaction)
        {
            this.transaction = transaction;
        }

        const string connString = @"Server=.\SQLEXPRESS;Database=EbecShopDB;Trusted_Connection=True;";
    }
}
