using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.Customer.BizLogic.Contract
{
    public interface ICustomerLogicObserver
    {
        void OnOrderCancelled(int orderId);
    }
}
