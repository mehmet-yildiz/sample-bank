using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SampleBank.Business;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Abstractions.Logging;
using SampleBank.Core.Entity;
using SampleBank.Persistence;
using SampleBank.Persistence.EF;
using Xunit;

namespace SampleBank.Tests.Business
{
    public class BusinessTests
    {
        private readonly IBusinessBase<Customer> _service;

        public BusinessTests()
        {
            var dbContext = new EfContext(new DbContextOptionsBuilder<EfContext>().Options);
            var uow = new EfUnitOfWork();
            var logger = new Logger();
            var repo = new RepositoryBase<Customer>(new EfRepository(dbContext));
            _service = new BusinessCustomer(repo, uow, logger);
        }

        [Theory, AutoData]
        public void GetAllCustomers_should_return_expected_result(int expectedId)
        {
            _service.Insert(new Customer { Id = expectedId, Name = "mehmet", Surname = "yildiz" });
            _service.Insert(new Customer { Id = 1, Name = "nurgul", Surname = "dereli" });

            //when
            var result = _service.GetAll();

            //then
            result.Data.First().Id.Should().Be(expectedId);
            result.Data.Count().Should().Be(2);
        }

        [Theory, AutoData]
        public void GetById_should_return_expected_result(int expectedId)
        {
            _service.Insert(new Customer { Id = expectedId, Name = "mehmet", Surname = "yildiz" });

            //when
            var result = _service.GetById(expectedId);

            //then
            result.Data.Id.Should().Be(expectedId);
            result.Data.Id.Should().NotBe(expectedId + 1);
        }

    }
}
