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
        public TeamRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public Team Get(int id)
        {
            return connection.Query<Team>($"SELECT * FROM Teams t WHERE t.Id = @Id", new { Id = id }, transaction: transaction).FirstOrDefault();
        }

        public IEnumerable<Team> GetAll()
        {
            return connection.Query<Team>("SELECT * FROM Teams").AsList();
        }

        public Team Add(Team team)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: team.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@Name", team.Name);
            parameters.Add("@Balance", team.Balance);
            parameters.Add("@BlockedBalance", team.BlockedBalance);

            connection.Execute("InsertTeam", parameters, transaction: transaction, commandType: CommandType.StoredProcedure);

            team.Id = parameters.Get<int>("@Id");
            return team;
        }

        public Team Update(Team team)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", team.Id);
            parameters.Add("@Name", team.Name);
            parameters.Add("@Balance", team.Balance);
            parameters.Add("@BlockedBalance", team.BlockedBalance);

            connection.Execute("UpdateTeam", parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
            return team;
        }

        public void Remove(int id)
        {
            connection.Execute("DELETE FROM Teams WHERE Id = @Id", new { Id = id }, transaction: transaction);
        }
              
        public Team GetTeam(int id)
        {
            using (var multipleResults = connection.QueryMultiple("GetTeam", new { Id = id }, transaction: transaction, commandType: CommandType.StoredProcedure))
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

        public void Save(Team team)
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
                        throw new NotImplementedException();
                        //DbContext.Participants.Add(member);
                    }
                    else
                    {
                        throw new NotImplementedException();
                        //DbContext.Participants.Update(member);
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
            limits = connection.Query<Tuple<int, decimal>>("SELECT ProductTypeId, Limit FROM TeamProductLimits WHERE TeamId = @Id", new { Id = teamId }, transaction: transaction).ToList();

            foreach(var limit in limits)
            {
                throw new NotImplementedException();
                //result.Add(
                //    DbContext.Products.Find(limit.Item1),
                //    limit.Item2
                //    );
            }
            return result;
        }

        public decimal GetProductLimitForTeam(Team team, ProductType product)
        {
            return GetProductLimitForTeam(team.Id, product.Id);
        }

        public decimal GetProductLimitForTeam(int teamId, int productTypeId)
        {
            return connection.Query<decimal>(
                "GetTeamProductTypeLimit", 
                new {
                    TeamId = teamId,
                    ProductTypeId = productTypeId
                },
                transaction: transaction,
                commandType: CommandType.StoredProcedure)
                    .FirstOrDefault();
        }
    }
}
