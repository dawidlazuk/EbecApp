using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.DataAccess.Queries
{
    public abstract class Query<T>
    {
        public abstract string GetSqlQuery();


        protected bool IsFirstContitionAppended;

        protected string GetSqlKeyword()
        {
            return IsFirstContitionAppended ? "AND" : "WHERE";
        }

        internal abstract Task<IEnumerable<T>> ExecuteAsync(IDbConnection connection);
    }
}
