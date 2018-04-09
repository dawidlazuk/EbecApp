using EbecShop.DataAccess.Repositiories;
using EbecShop.DataAccess.Repositiories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.DataAccess
{
    public static class DbContext
    {
        //TODO remove to config
        const string connString = @"Server=.\SQLEXPRESS;Database=EbecShopDB;Trusted_Connection=True;";
                
        public static ITeamRepository Teams { get; private set; }
        public static IParticipantRepository Participants { get; private set; }
        public static IOrderRepository Orders { get; private set; }
        public static IProductRepository Products { get; private set; }          
        public static IProductTypeRepository ProductTypes { get; private set; }


        static DbContext()
        {
            Teams = new TeamRepository();
            Participants = new ParticipantRepository();
            Orders = new OrderRepository();
            Products = new ProductRepository();
            ProductTypes = new ProductTypeRepository();
        }

        public static void ExecuteAsTransaction(Action<IDbConnection> action)
        {
            using (var connection = new SqlConnection(connString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        action?.Invoke(connection);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Debug.WriteLine(ex.ToString());
                    }
                }
            }
        }

    }
}
