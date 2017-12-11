using EbecApp.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbecApp.Model.Repositiories.Interfaces
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
