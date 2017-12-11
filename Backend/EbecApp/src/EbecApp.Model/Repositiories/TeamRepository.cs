using EbecApp.Model.Repositiories.Interfaces;
using System;
using System.Collections.Generic;
using EbecApp.Model.Types;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace EbecApp.Model.Repositiories
{
    public class TeamRepository : ITeamRepository
    {
        const string connString = "Server=.\SQLEXPRESS;Database=EbecShopDB;";

        private IDbConnection db = new SqlConnection(connString);
     
        public Team Find(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Team> GetAll()
        {
            return this.db.Query<Team>("SELECT * FROM Teams").AsList();
        }

        public Team Add(Team team)
        {
            throw new NotImplementedException();
        }

        public Team Update(Team team)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }
              
        public Team GetFullTeam(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Team team)
        {
            throw new NotImplementedException();
        }    
    }
}
