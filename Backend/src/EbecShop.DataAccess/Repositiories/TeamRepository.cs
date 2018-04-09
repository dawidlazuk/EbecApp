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
            using (var connection = CreateDbConnection())
                return connection.Query<Team>($"SELECT * FROM Teams t WHERE t.Id = @Id", new { Id = id }).FirstOrDefault();
        }

        public IEnumerable<Team> GetAll()
        {
            using (var connection = CreateDbConnection())
                return connection.Query<Team>("SELECT * FROM Teams").AsList();
        }

        public Team Add(Team team)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: team.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@Name", team.Name);
            parameters.Add("@Balance", team.Balance);
            parameters.Add("@BlockedBalance", team.BlockedBalance);

            using (var connection = CreateDbConnection())
                connection.Execute("InsertTeam", parameters, commandType: CommandType.StoredProcedure);

            team.Id = parameters.Get<int>("@Id");
            return team;
        }

        public Team Update(Team team, IDbConnection connection = null)
        {
            PerformOnDatabase(conn => conn.Execute("UpdateTeam", team, commandType: CommandType.StoredProcedure), connection);
            return team;
        }

        public void Remove(int id)
        {
            using (var connection = CreateDbConnection())
                connection.Execute("DELETE FROM Teams WHERE Id = @Id", new { Id = id });
        }
              
        public Team GetTeam(int id)
        {
            using (var connection = CreateDbConnection())
            {
                using (var multipleResults = connection.QueryMultiple("GetTeam", new { Id = id }, commandType: CommandType.StoredProcedure))
                {
                    var team = multipleResults.Read<Team>().SingleOrDefault();
                    var members = multipleResults.Read<Participant>().ToList();

                    if (team != null && members != null)
                    {
                        members.ForEach(p => p.Team = team);
                        team.Members.AddRange(members);
                    }
                    return team;
                }
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

            List<Tuple<int, decimal>> limits;
            using (var connection = CreateDbConnection())
                limits = connection.Query<Tuple<int, decimal>>("SELECT ProductTypeId, Limit FROM TeamProductLimits WHERE TeamId = @Id", new { Id = teamId }).ToList();

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
            using (var connection = CreateDbConnection())
                return connection.Query<decimal>(
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
