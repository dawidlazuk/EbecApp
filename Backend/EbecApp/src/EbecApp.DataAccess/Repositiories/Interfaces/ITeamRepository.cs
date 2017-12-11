using System.Collections.Generic;

using EbecApp.Model;

namespace EbecApp.DataAccess.Repositiories.Interfaces
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
    }
}
