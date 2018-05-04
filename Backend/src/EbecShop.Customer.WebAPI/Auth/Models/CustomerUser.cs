using EbecShop.Model;
using System.ComponentModel.DataAnnotations;

namespace EbecShop.WebAPI.Auth.Models
{
    public class CustomerUser : User
    {
        public int TeamId { get; set; }

        [Required]
        public string TeamName { get; set; }
    }
}
