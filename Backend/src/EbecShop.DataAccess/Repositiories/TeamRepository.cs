using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Dapper;


using EbecShop.Model;
using EbecShop.DataAccess.Repositiories.Abstract;
using EbecShop.DataAccess.Repositiories.Interfaces;
using System.Transactions;
using System;

namespace EbecShop.DataAccess.Repositiories
{
    public class TeamRepository : Repository, ITeamRepository
    {
        public Team Find(int id)
        {
            return this.db.Query<Team>($"SELECT * FROM Teams t WHERE t.Id = @Id", new { Id = id }).FirstOrDefault();
        }

        public IEnumerable<Team> GetAll()
        {
            return this.db.Query<Team>("SELECT * FROM Teams").AsList();
        }

        public Team Add(Team team)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: team.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@Name", team.Name);
            parameters.Add("@Balance", team.Balance);
            parameters.Add("@BlockedBalance", team.BlockedBalance);
            this.db.Execute("InsertTeam", parameters, commandType: CommandType.StoredProcedure);
            team.Id = parameters.Get<int>("@Id");

            return team;
        }

        public Team Update(Team team)
        {
            this.db.Execute("UpdateTeam", team, commandType: CommandType.StoredProcedure);
            return team;
        }

        public void Remove(int id)
        {
            this.db.Execute("DELETE FROM Teams WHERE Id = @Id", new { Id = id });
        }
              
        public Team GetFullTeam(int id)
        {
            using (var multipleResults = this.db.QueryMultiple("GetTeam", new { Id = id }, commandType: CommandType.StoredProcedure))
            {
                var team = multipleResults.Read<Team>().SingleOrDefault();
                var members = multipleResults.Read<Participant>().ToList();

                if(team != null && members != null)
                {
                    members.ForEach(p => p.Team = team);
                    team.Members.AddRange(members);
                }

                return team;
            }
        }

        public void Save(Team team)
        {
            //.NET Core 2.0 feature - uncomment in VS2017
            using (var txScope = new TransactionScope())
            {                
                if (team.IsNew)
                    this.Add(team);
                else
                    this.Update(team);

                foreach (var member in team.Members)
                {
                    if (member.IsDeleted == false)
                    {
                        if (member.IsNew)
                        {
                            DbContext.Participants.Add(member);
                        }
                        else
                        {
                            DbContext.Participants.Update(member);
                        }
                    }
                }

            }            
        }

        public IDictionary<Product, decimal> GetTeamLimits(Team team)
        {
            return GetTeamLimits(team.Id);
        }
        public IDictionary<Product, decimal> GetTeamLimits(int teamId)
        {
            IDictionary<Product, decimal> result = new Dictionary<Product, decimal>();

            var limits = this.db.Query<Tuple<int, decimal>>("SELECT ProductTypeId, Limit FROM TeamProductLimits WHERE TeamId = @Id", new { Id = teamId }).ToList();

            foreach(var limit in limits)
            {
                result.Add(
                    DbContext.Products.Find(limit.Item1),
                    limit.Item2
                    );
            }
            return result;
        }

        public decimal GetProductLimitForTeam(Team team, ProductType product)
        {
            return GetProductLimitForTeam(team.Id, product.Id);
        }

        public decimal GetProductLimitForTeam(int teamId, int productTypeId)
        {
            return this.db.Query<decimal>(
                "GetTeamProductTypeLimit", 
                new {
                    TeamId = teamId,
                    ProductTypeId = productTypeId
                },
                commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
        }
    }
}
