using Microsoft.AspNetCore.Identity;

namespace EbecShop.WebAPI.Auth.Models
{
    public abstract class User
    {
        public int Id { get; set; }
        public string IdentityId { get; set; }
    }
}
