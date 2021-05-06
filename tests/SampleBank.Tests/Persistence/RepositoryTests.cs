using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SampleBank.Core.Entity;
using SampleBank.Persistence;
using SampleBank.Persistence.EF;
using FluentAssertions;
using Xunit;

namespace SampleBank.Tests.Persistence
{
    public class RepositoryTests
    {
        private EfContext CreateDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<EfContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new EfContext(options);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(5)]
        [InlineData(10)]
        public void GetAll_customers(int count)
        {
            using (var context = CreateDbContext($"GetAll_customers{count}"))
            {
                for (var i = 0; i < count; i++)
                {
                    context.Set<Customer>().Add(new Customer());
                }
                context.SaveChanges();
            }
            List<Customer> customers = null;

            using (var context = CreateDbContext($"GetAll_customers{count}"))
            {
                var repository = new RepositoryCustomer(new EfRepository(context));
                customers = repository.GetAll().ToList();
            }
            customers.Should().NotBeNull();
            customers.Count.Should().Be(count);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GetById_existing_customers(int id)
        {
            using (var context = CreateDbContext("GetById_existing_customers"))
            {
                context.Set<Customer>().Add(new Customer { Id = id });
                context.SaveChanges();
            }
            Customer customer = null;

            using (var context = CreateDbContext("GetById_existing_customers"))
            {
                var repository = new RepositoryCustomer(new EfRepository(context));
                customer = repository.GetById(id);
            }

            customer.Should().NotBeNull();
            customer.Id.Should().Be(id);
        }


        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void GetById_nonexisting_customers(int id)
        {
            Customer customer = null;

            using (var context = CreateDbContext("GetById_nonexisting_customers"))
            {
                var repository = new RepositoryCustomer(new EfRepository(context));
                customer = repository.GetById(id);
            }

            customer.Should().BeNull();
        }

        [Fact]
        public void Create_Customer()
        {
            var entity = new Customer
            {
                Name = "nurgul",
                Surname = "dereli"
            };

            using (var context = CreateDbContext("Create_Customer"))
            {
                var repository = new RepositoryCustomer(new EfRepository(context));
                repository.Insert(entity);
            }
            entity.Id.Should().BeGreaterThan(0);

            using (var context = CreateDbContext("Create_Customer"))
            {
                context.Customers.Count().Should().Be(1);
                context.Customers.First().Should().BeEquivalentTo(entity);
            }
        }

        [Fact]
        public void Update_Customer()
        {
            int id;
            string name = "";
            using (var context = CreateDbContext("Update_Customer"))
            {
                var createdCustomer = new Customer
                {
                    Name = "nurgul",
                    Surname = "dereli"
                };
                context.Set<Customer>().Add(createdCustomer);
                context.Set<Customer>().Add(new Customer { Name = "Another Name", Surname = "Another Surname" });
                context.SaveChanges();

                id = createdCustomer.Id;
                name = createdCustomer.Name;
            }

            Customer updateCustomer;
            using (var context = CreateDbContext("Update_Customer"))
            {
                updateCustomer = context.Set<Customer>().FirstOrDefault(x => x.Id == id);
                updateCustomer.Name = "mehmet";
                updateCustomer.Surname = "yildiz";
                var repository = new RepositoryCustomer(new EfRepository(context));
                repository.Update(updateCustomer);
            }

            Customer savedCustomer;
            using (var context = CreateDbContext("Update_Customer"))
            {
                var repository = new RepositoryCustomer(new EfRepository(context));
                savedCustomer = repository.GetById(id);
            }

            savedCustomer.Name.Should().NotBeEquivalentTo(name);
            using (var context = CreateDbContext("Update_Customer"))
            {
                context.Customers.First(x => x.Id == updateCustomer.Id).Should().BeEquivalentTo(updateCustomer);
            }
        }

        [Fact]
        public void Delete_Customer()
        {
            int result;
            int id;
            using (var context = CreateDbContext("Delete_Customer"))
            {
                var createdCustomer = new Customer
                {
                    Name = "nurgul",
                    Surname = "dereli"
                };
                context.Set<Customer>().Add(createdCustomer);
                context.Set<Customer>().Add(new Customer { Name = "Another Name", Surname = "Another Surname" });
                context.SaveChangesAsync();
                id = createdCustomer.Id;
            }

            using (var context = CreateDbContext("Delete_Customer"))
            {
                var repository = new RepositoryCustomer(new EfRepository(context));
                var customer = repository.GetById(id);
                repository.Delete(customer);
            }

            // Simulate access from another context to verifiy that correct data was saved to database
            using (var context = CreateDbContext("Delete_Customer"))
            {
                ( context.Set<Customer>().FirstOrDefault(x => x.Id == id)).Should().BeNull();
                context.Set<Customer>().ToList().Should().NotBeEmpty();
            }
        }


    }
}
