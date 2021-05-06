using Microsoft.EntityFrameworkCore;
using SampleBank.Core.Entity;
using SampleBank.Persistence.Extensions;

namespace SampleBank.Persistence.EF
{
    public sealed class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Claim> Claims { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //allows Entity Framework Core to be used with an in-memory database (for testing purpose)
            optionsBuilder.UseInMemoryDatabase("SampleBankInMemoryDatabase");
        }
    }
}