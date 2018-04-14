using EbecShop.Model.Abstract;
using EbecShop.Model.Enums;
using System.Collections.Generic;
using System.Linq;

namespace EbecShop.Model
{

    public class Order : Entity
    {
        private Team _team;

        public int TeamId { get; set; }
        public Team Team
        {
            get { return _team; }
            set
            {
                _team = value;
                TeamId = _team.Id;
            }
        }

        public OrderStatus Status { get; set; }
        

        public IDictionary<ProductType, decimal> Products { get; set; }

        /// <summary>
        /// TODO, not implemented
        /// </summary>
        public string Comment { get; set; }

        public Order()
        {

        }

        public decimal Value => Products.Sum(pa => pa.Key.Price * pa.Value);

        public void AddComment(string comment)
        {
            if (string.IsNullOrEmpty(this.Comment))
                this.Comment = comment;
            else
                this.Comment += $"\n{comment}";
        }
    }
}
