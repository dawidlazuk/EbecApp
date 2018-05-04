namespace EbecShop.WebAPI.Auth.Models
{
    public enum UserType
    {
        Customer = 1,
        Salesman = 2
    }

    public class CustomerRegistrationRequest
    {
        public CustomerUser User { get; set; }
        public string Password { get; set; }

    }
}