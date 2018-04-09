using EbecShop.Model;
using System;

namespace EbecShop.DataAccess
{
    public static class DbAccessLayer
    {
        public static void AddNewOrderToDatabase(Team team, Order order)
        {
            DbContext.ExecuteAsTransaction((connection) =>
            {
                team.BlockedBalance += order.Value;
                order = DbContext.Orders.Add(order, connection);
                throw new Exception();
                team = DbContext.Teams.Update(team, connection);
            });
        }
    }
}
