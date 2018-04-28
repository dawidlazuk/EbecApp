using EbecShop.WebAPI.Auth.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EbecShop.WebAPI.Auth.DbContext
{
    public class AuthDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<SalesmanUser> Salesmen { get; set; }
        public DbSet<CustomerUser> Customers { get; set; }
    }
}
