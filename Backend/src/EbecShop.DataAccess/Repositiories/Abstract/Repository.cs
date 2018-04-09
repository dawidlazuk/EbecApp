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
        const string connString = @"Server=.\SQLEXPRESS;Database=EbecShopDB;Trusted_Connection=True;";

        protected IDbConnection CreateDbConnection() => new SqlConnection(connString);

        protected void PerformOnDatabase(Action<IDbConnection> action, IDbConnection connection = null)
        {
            bool isConnectionInjected = connection != null;
            if (connection == null)
                connection = CreateDbConnection();

            action(connection);

            if (isConnectionInjected)
                connection.Dispose();
        }
    }
}
