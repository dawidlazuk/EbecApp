using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.Customer.BizLogic.Contract
{
    public class DefaultCustomerLogicObserver : ICustomerLogicObserver
    {
        public void OnOrderCancelled(int orderId)
        {            
        }
    }
}
