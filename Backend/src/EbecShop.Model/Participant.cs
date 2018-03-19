using EbecShop.Model.Abstract;

namespace EbecShop.Model
{
    public class Participant : Entity
    {
        public string Firstname { get; set; }
        public string Surname { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }

        public bool IsDeleted { get; set; }
       
    }
}
