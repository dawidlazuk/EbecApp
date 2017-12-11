﻿using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using Dapper;

using EbecApp.Model;
using EbecApp.DataAccess.Repositiories.Interfaces;

namespace EbecApp.DataAccess.Repositiories
{
    public class TeamRepository : ITeamRepository
    {
        const string connString = @"Server=.\SQLEXPRESS;Database=EbecShopDB;Trusted_Connection=True;";

        private IDbConnection db = new SqlConnection(connString);
     
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
            var sql = "INSERT INTO Teams (Name, Balance) VALUES(@Name, @Balance);"+
                               "SELECT CAST(SCOPE_IDENTITY() as int);";
            var id = this.db.Query<int>(sql, team).Single();
            team.Id = id;
            return team;
        }

        public Team Update(Team team)
        {
            var sql = "UPDATE Teams " +
                      "SET Name     = @Name, " +
                      "    Balance  = @Balance " +
                      "WHERE Id = @Id";
            this.db.Execute(sql, team);
            return team;
        }

        public void Remove(int id)
        {
            this.db.Execute("DELETE FROM Teams WHERE Id = @Id", new { Id = id });
        }
              
        public Team GetFullTeam(int id)
        {
            var sql = "SELECT * FROM Teams WHERE ID = @Id;" +
                      "SELECT * FROM Participants WHERE TeamID = @Id";

            using (var multipleResults = this.db.QueryMultiple(sql, new { Id = id }))
            {
                var team = multipleResults.Read<Team>().SingleOrDefault();
                var members = multipleResults.Read<Participant>().ToList();

                if(team != null && members != null)
                {
                    team.Members.AddRange(members);
                }

                return team;
            }
        }

        public void Save(Team team)
        {
            //.NET Core 2.0 feature - uncomment in VS2017
            //using(var txScope = new TransactionScope())
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
                            //participantsRepository.Add(member);
                        }
                        else
                        {
                            //participantsRepository.Update(member);
                        }
                    }
                    else
                    {
                        //participantsRepository.Remove(member);
                    }
                }

            }            
        }        
    }
}
