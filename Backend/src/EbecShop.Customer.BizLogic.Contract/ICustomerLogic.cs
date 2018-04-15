using EbecShop.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        Order GetOrder(int id);

        Order CreateOrder(
            Team team,
            IDictionary<ProductType, decimal> products
            );

        Order CreateOrder(
            int teamId,
            IDictionary<int, decimal> products
            );

        Task<Order> CancelOrder(int orderId);
        Order CancelOrder(Order order);

        Task<IEnumerable<Order>> GetTeamOrders(Team team);

        Team GetTeam(int id);
    }
}
