using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbecShop.Model;
using EbecShop.DataAccess.Repositiories.Interfaces;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using EbecShop.DataAccess.Repositiories.Abstract;

namespace EbecShop.DataAccess.Repositiories
{ 
    public class ParticipantRepository : Repository, IParticipantRepository
    {
        public Participant Add(Participant participant)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: participant.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@Firstname", participant.Firstname);
            parameters.Add("@Surname", participant.Surname);
            parameters.Add("@TeamId", participant.TeamId);
            this.db.Execute("InsertParticipant", parameters, commandType: CommandType.StoredProcedure);
            participant.Id = parameters.Get<int>("@Id");

            return participant;
        }

        public Participant Find(int id)
        {
            return this.db.Query<Participant>("SELECT * FROM Participants WHERE Id=@Id", new { Id = id }).FirstOrDefault();
        }

        public Participant Update(Participant participant)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", participant.Id);
            parameters.Add("@Firstname", participant.Firstname);
            parameters.Add("@Surname", participant.Surname);
            parameters.Add("@TeamId", participant.TeamId);
            this.db.Execute("UpdateParticipant", parameters, commandType: CommandType.StoredProcedure);
            return participant;
        }

        public IEnumerable<Participant> GetAll()
        {
            return this.db.Query<Participant>("SELECT * FROM Participants").AsList();
        }

        public Participant GetFullParticipant(int id)
        {
            var participant = Find(id);
            participant.Team = DbContext.Teams.GetFullTeam(participant.TeamId);
            return participant;
        }

        public void Save(Participant participant)
        {           
            //.NET Core 2.0 feature - uncomment in VS2017
            //using(var txScope = new TransactionScope())
            {
                if (participant.IsNew)
                    this.Add(participant);
                else
                    this.Update(participant);
            }
        }      
    }
}
