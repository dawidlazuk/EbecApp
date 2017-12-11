using EbecApp.Model.Types.Abstract;

namespace EbecApp.Model.Types
{
    public class Participant : Entity
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public Team Team { get; set; }
    }
}
