using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleBank.Core.Entity;
using SampleBank.Persistence;
using SampleBank.Persistence.EF;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace SampleBank.Tests.Persistence
{
    public class RepositoryTests
    {
        private EfContext CreateDbContext(string name = "SampleBankInMemoryDatabase")
        {
            var options = new DbContextOptionsBuilder<EfContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new EfContext(options);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(10)]
        public void GetAll_customers(int count)
        {
            using (var context = CreateDbContext($"SampleBankInMemoryDatabase_{count}"))
            {
                for (var i = 0; i < count; i++)
                {
                    context.Set<Customer>().Add(new Customer());
                }
                context.SaveChanges();
            }
            List<Customer> customers = null;

            using (var context = CreateDbContext($"SampleBankInMemoryDatabase_{count}"))
            {
                var repository = new RepositoryCustomer(new EfRepository(context));
                customers = repository.GetAll().ToList();
            }
            customers.Should().NotBeNull();
            customers.Count.Should().Be(count);
        }


        [Theory]
        [InlineData(10)]
        [InlineData(11)]
        [InlineData(12)]
        [InlineData(13)]
        [InlineData(14)]
        [InlineData(15)]
        public void GetById_existing_customers(int id)
        {
            using (var context = CreateDbContext())
            {
                context.Set<Customer>().Add(new Customer { Id = id });
                context.SaveChanges();
            }
            Customer customer = null;

            using (var context = CreateDbContext())
            {
                var repository = new RepositoryCustomer(new EfRepository(context));
                customer = repository.GetById(id);
            }

            customer.Should().NotBeNull();
            customer.Id.Should().Be(id);
        }
    }
}
