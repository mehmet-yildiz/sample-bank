using System;
using Microsoft.EntityFrameworkCore;
using SampleBank.Core.Entity;
using SampleBank.Core.Helpers;

namespace SampleBank.Persistence.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SeedInMemoryData(this ModelBuilder modelBuilder)
        {
            HashingHelper.CreatePasswordHash("123", out var passwordHash, out var passwordSalt);

            var user1 = new User
            {
                Id = 1,
                Name = "mehmet",
                Surname = "yildiz",
                Username = "mehmet",
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            var customer1 = new Customer
            {
                Id = 1,
                Name = "John",
                Surname = "Doe"
            };
            var customer2 = new Customer
            {
                Id = 2,
                Name = "Rita",
                Surname = "Curie"
            };

            var account1 = new Account
            {
                Id = 1,
                Balance = 500,
                InitialCredit = 0,
                CustomerId = customer1.Id
            };
            var account2 = new Account
            {
                Id = 2,
                Balance = 1500,
                InitialCredit = 100,
                CustomerId = customer2.Id
            };
            var account3 = new Account
            {
                Id = 3,
                Balance = 4500,
                InitialCredit = 0,
                CustomerId = customer2.Id
            };

            var transaction1 = new Transaction { Id = 1, ProcessName = "MoneyTransfer", ProcessDate = DateTime.Now, AccountId = 2 };
            var transaction2 = new Transaction { Id = 2, ProcessName = "MoneyTransfer", ProcessDate = DateTime.Now, AccountId = 2 };
            var transaction3 = new Transaction { Id = 3, ProcessName = "Swift", ProcessDate = DateTime.Now, AccountId = 2 };
            var transaction4 = new Transaction { Id = 4, ProcessName = "Payment", ProcessDate = DateTime.Now, AccountId = 2 };

            modelBuilder.Entity<User>().HasData(user1);
            modelBuilder.Entity<Customer>().HasData(customer1, customer2);
            modelBuilder.Entity<Account>().HasData(account1, account2, account3);
            modelBuilder.Entity<Transaction>().HasData(transaction1, transaction2, transaction3, transaction4);
        }
    }
}