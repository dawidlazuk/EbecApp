using EbecApp.Model.Abstract;

namespace EbecApp.Model
{
    public class Participant : Entity
    {
        public string Firstname { get; set; }
        public bool IsNew { get; internal set; }
        public string Surname { get; set; }
        public int TeamId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
