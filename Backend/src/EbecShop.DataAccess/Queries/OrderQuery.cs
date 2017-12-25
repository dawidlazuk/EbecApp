using EbecShop.Model.Enums;
using System.Text;

namespace EbecShop.DataAccess.Queries
{
    public class OrderQuery : Query
    {
        public OrderStatus? Status { get; set; }

        public int? TeamId { get; set; }
        

        public override string GetSqlQuery()
        {
            IsFirstContitionAppended = false;
            var builder = new StringBuilder();
            builder.Append("SELECT * FROM Orders ");

            if (Status != null)
            {
                builder.Append($"{GetSqlKeyword()} Status = @Status ");
                IsFirstContitionAppended = true;
            }

            if (TeamId != null)
                builder.Append($"{GetSqlKeyword()} TeamId = @TeamId");

            return builder.ToString();
        }      
    }
}