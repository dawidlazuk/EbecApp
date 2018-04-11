using Dapper;
using EbecShop.Model;
using EbecShop.Model.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EbecShop.DataAccess.Queries
{
    public class OrderQuery : Query<Order>
    {
        public int? OrderId { get; set; }
        public int? TeamId { get; set; }
        public OrderStatus? Status { get; set; }
        
        public override DynamicParameters Parameters
        {
            get
            {
                var parameters = new DynamicParameters();
                if (OrderId.HasValue)
                    parameters.Add("@Id", OrderId);

                if (Status.HasValue)
                    parameters.Add("@Status", Status);

                if (TeamId.HasValue)
                    parameters.Add("@TeamId", TeamId);
                return parameters;
            }
        }
    }
}