using EbecShop.Model;
using System.Collections.Generic;

namespace EbecShop.Customer.BizLogic.Contract
{
    public interface ICustomerLogic
    {
        /*
         * Customer functionalities:
         *  1. Get all products.
         *  2. Get product details.
         *  3. Show customer orders.
         *  4. Make new order.
         *  5. Cancel order.
         *  6. Get customer limit.
         */


        IEnumerable<Product> GetProducts();

        Product GetProduct(int id);

        Order CreateOrder(
            Team team,
            IDictionary<Product, decimal> products
            );

        Order CancelOrder(Order order);

        IEnumerable<Order> GetTeamOrders(Team team);
    }
}
