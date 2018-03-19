using EbecShop.DataAccess.Repositiories;
using EbecShop.DataAccess.Repositiories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.DataAccess
{
    public static class DbContext
    {
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
    }
}
