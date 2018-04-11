using Dapper;

namespace EbecShop.DataAccess.Queries
{
    public abstract class Query<T>
    {
        public abstract DynamicParameters Parameters { get; }
    }
}
