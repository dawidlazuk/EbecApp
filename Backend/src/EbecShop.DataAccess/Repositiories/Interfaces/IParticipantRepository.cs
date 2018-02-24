using EbecShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EbecShop.DataAccess.Repositiories.Interfaces
{
    public interface IParticipantRepository
    {
        Participant Find(int id);
        IEnumerable<Participant> GetAll();
        Participant Add(Participant participant);
        Participant Update(Participant participant);

        Participant GetFullParticipant(int id);
        void Save(Participant participant);
    }
}
