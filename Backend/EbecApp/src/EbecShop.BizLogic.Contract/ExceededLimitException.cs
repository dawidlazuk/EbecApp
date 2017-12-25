using EbecShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.BizLogic.Contract
{
    public class ExceededLimitException : Exception
    {
        public Team Team { get; set; }
        public Product Product { get; set; }
        public Decimal OrderedAmount { get; set; }
        public Decimal Limit { get; set; }
    }
}
