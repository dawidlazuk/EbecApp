using System.Collections.Generic;

using EbecShop.Model;

namespace EbecShop.DataAccess.Repositiories.Interfaces
{
    public interface ITeamRepository
    {
        Team Find(int id);
        IEnumerable<Team> GetAll();
        Team Add(Team team);
        Team Update(Team team);
        void Remove(int id);

        Team GetFullTeam(int id);
        void Save(Team team);

        IDictionary<Product, decimal> GetTeamLimits(int teamId);
        IDictionary<Product, decimal> GetTeamLimits(Team team);

        decimal GetProductLimitForTeam(Team team, Product product);
        decimal GetProductLimitForTeam(int teamId, int productId);

    }
}
