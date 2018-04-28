using EbecShop.Model;

namespace EbecShop.WebAPI.Auth.Models
{
    public class CustomerUser : User
    {
        public int TeamId { get; set; }
    }
}
