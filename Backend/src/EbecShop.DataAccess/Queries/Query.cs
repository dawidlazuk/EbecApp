using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.DataAccess.Queries
{
    public abstract class Query
    {
        public abstract string GetSqlQuery();


        protected bool IsFirstContitionAppended;

        protected string GetSqlKeyword()
        {
            return IsFirstContitionAppended ? "AND" : "WHERE";
        }

    }
}
