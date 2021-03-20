using BetaBrew.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BetaBrew.Data
{
    public class BetaBrewDbContext : IdentityDbContext
    {
        public BetaBrewDbContext() {}

        public BetaBrewDbContext(DbContextOptions options) : base(options) { }
        
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }
    }
}