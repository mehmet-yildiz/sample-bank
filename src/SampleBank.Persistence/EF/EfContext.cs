using Microsoft.EntityFrameworkCore;
using SampleBank.Core.Entity;

namespace SampleBank.Persistence.EF
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options)
            : base(options)
        { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //allows Entity Framework Core to be used with an in-memory database (for testing purpose)
            optionsBuilder.UseInMemoryDatabase("SampleBankInMemoryDatabase");
        }
    }
}