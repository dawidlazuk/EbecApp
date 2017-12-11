using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EbecApp.Model;
using EbecApp.DataAccess.Repositiories.Interfaces;
using System.Data;
using System.Data.SqlClient;
using Dapper;

namespace EbecApp.DataAccess.Repositiories
{ 
    public class ParticipantRepository : IParticipantRepository
    {
        const string connString = @"Server=.\SQLEXPRESS;Database=EbecShopDB;Trusted_Connection=True;";

        private IDbConnection db = new SqlConnection(connString);

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

        public IEnumerable<Participant> GetAll()
        {
            return this.db.Query<Participant>("SELECT * FROM Participants").AsList();
        }

        public Participant GetFullParticipant(int id)
        {
            throw new NotImplementedException();
        }

        public void Save(Participant participant)
        {
            throw new NotImplementedException();
        }

        public Participant Update(Participant participant)
        {
            throw new NotImplementedException();
        }
    }
}
