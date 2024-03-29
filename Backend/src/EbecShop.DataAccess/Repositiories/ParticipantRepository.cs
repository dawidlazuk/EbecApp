﻿using System;
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
        public ParticipantRepository(IDbTransaction transaction) : base(transaction)
        {
        }

        public Participant Add(Participant participant)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", value: participant.Id, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
            parameters.Add("@Firstname", participant.Firstname);
            parameters.Add("@Surname", participant.Surname);
            parameters.Add("@TeamId", participant.TeamId);

            connection.Execute("InsertParticipant", parameters, transaction: transaction, commandType: CommandType.StoredProcedure);

            participant.Id = parameters.Get<int>("@Id");
            return participant;
        }

        public Participant Find(int id)
        {
            return connection.Query<Participant>("SELECT * FROM Participants WHERE Id=@Id", new { Id = id }, transaction: transaction).FirstOrDefault();
        }

        public Participant Update(Participant participant)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", participant.Id);
            parameters.Add("@Firstname", participant.Firstname);
            parameters.Add("@Surname", participant.Surname);
            parameters.Add("@TeamId", participant.TeamId);

            connection.Execute("UpdateParticipant", parameters, transaction: transaction, commandType: CommandType.StoredProcedure);
            return participant;
        }

        public IEnumerable<Participant> GetAll()
        {
            return connection.Query<Participant>("SELECT * FROM Participants", transaction: transaction).AsList();
        }
        
        public void Save(Participant participant)
        {           
            {
                if (participant.IsNew)
                    this.Add(participant);
                else
                    this.Update(participant);
            }
        }      
    }
}
