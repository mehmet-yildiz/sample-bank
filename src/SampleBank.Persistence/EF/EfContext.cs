using System;
using Microsoft.EntityFrameworkCore;
using SampleBank.Core.Entity;
using System.Linq;

namespace SampleBank.Persistence.EF
{
    public class EfContext : DbContext
    {
        public EfContext(DbContextOptions<EfContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Claim> Claims { get; set; }
        public virtual DbSet<UserClaim> UserClaims { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //database must be unique for repository tests. therefore storename is checked.
            var storeName = ((Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal.InMemoryOptionsExtension)
                optionsBuilder.Options.Extensions.FirstOrDefault(x =>
                    x.GetType() ==
                    typeof(Microsoft.EntityFrameworkCore.InMemory.Infrastructure.Internal.
                        InMemoryOptionsExtension)))?.StoreName;

            //allows Entity Framework Core to be used with an in-memory database (for testing purpose)
            optionsBuilder.UseInMemoryDatabase(storeName ?? "SampleBankInMemoryDatabase");
        }
    }
}