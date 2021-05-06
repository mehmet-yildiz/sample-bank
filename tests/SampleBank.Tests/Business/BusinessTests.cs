using System;
using System.Linq;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using SampleBank.Business;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Abstractions.Logging;
using SampleBank.Core.Entity;
using SampleBank.Core.Enums;
using SampleBank.Persistence;
using SampleBank.Persistence.EF;
using Xunit;

namespace SampleBank.Tests.Business
{
    public class BusinessTests
    {
        private EfContext CreateDbContext(string name)
        {
            var options = new DbContextOptionsBuilder<EfContext>()
                .UseInMemoryDatabase(name)
                .Options;
            return new EfContext(options);
        }

        private readonly IBusinessBase<Customer> _serviceCustomer;
        private readonly IBusinessBase<Account> _serviceAccount;
        private readonly IBusinessBase<Transaction> _serviceTransaction;

        public BusinessTests()
        {
            var dbContext = new EfContext(new DbContextOptionsBuilder<EfContext>().Options);
            var uow = new EfUnitOfWork();
            var logger = new Logger();
            var repoCustomer = new RepositoryBase<Customer>(new EfRepository(dbContext));
            var repoAccount = new RepositoryBase<Account>(new EfRepository(dbContext));
            var repoTransaction = new RepositoryTransaction(new EfRepository(dbContext));
            _serviceCustomer = new BusinessCustomer(repoCustomer, uow, logger);
            _serviceAccount = new BusinessAccount(repoAccount, uow, logger, repoCustomer, repoTransaction);
            _serviceTransaction = new BusinessBase<Transaction>(repoTransaction, uow, logger);
        }

        [Theory, AutoData]
        public void GetAllCustomers_should_return_expected_result(int expectedId)
        {
            _serviceCustomer.Insert(new Customer { Id = expectedId, Name = "mehmet", Surname = "yildiz" });
            _serviceCustomer.Insert(new Customer { Id = 1, Name = "nurgul", Surname = "dereli" });

            //when
            var result = _serviceCustomer.GetAll();

            //then
            result.Data.First().Id.Should().Be(expectedId);
            result.Data.Count().Should().Be(2);
        }

        [Theory, AutoData]
        public void GetCustomerById_should_return_expected_result(int expectedId)
        {
            _serviceCustomer.Insert(new Customer { Id = expectedId, Name = "mehmet", Surname = "yildiz" });

            //when
            var result = _serviceCustomer.GetById(expectedId);

            //then
            result.Data.Id.Should().Be(expectedId);
            result.Data.Id.Should().NotBe(expectedId + 1);
        }

        [Theory, AutoData]
        public void GetAllAccounts_should_return_expected_result(int newId)
        {
            _serviceAccount.Insert(new Account { Id = newId, Balance = 100, CustomerId = 1, InitialCredit = 50 });
            _serviceAccount.Insert(new Account { Id = 1, Balance = 50, CustomerId = 3, InitialCredit = 500 });

            //when
            var result = _serviceAccount.GetAll();

            //then
            result.Data.Any(x => x.Id == newId).Should().BeTrue();
            result.Data.Count().Should().BeGreaterOrEqualTo(2);
        }

        [Theory, AutoData]
        public void GetByIdAccount_should_return_expected_result(int expectedId)
        {
            _serviceAccount.Insert(new Account { Id = expectedId, Balance = 1, CustomerId = 2, InitialCredit = 3 });

            //when
            var result = _serviceAccount.GetById(expectedId);

            //then
            result.Data.Id.Should().Be(expectedId);
            result.Data.Id.Should().NotBe(expectedId + 1);
        }

        [Theory, AutoData]
        public void GetAllTransactions_should_return_expected_result(int newId)
        {
            _serviceTransaction.Insert(new Transaction { Id = newId,AccountId = 1, ProcessDate = DateTime.Now, TransactionProcess = ProcessesType.Check});
            _serviceTransaction.Insert(new Transaction { Id = 1, AccountId = 2, ProcessDate = DateTime.Now, TransactionProcess = ProcessesType.CreditRequest});

            //when
            var result = _serviceTransaction.GetAll();

            //then
            result.Data.Any(x => x.Id == newId).Should().BeTrue();
            result.Data.Count().Should().BeGreaterOrEqualTo(2);
        }

        [Theory, AutoData]
        public void GetByIdTransaction_should_return_expected_result(int expectedId)
        {
            _serviceTransaction.Insert(new Transaction { Id = expectedId, AccountId = 3, ProcessDate = DateTime.Now, TransactionProcess = ProcessesType.Payment});

            //when
            var result = _serviceTransaction.GetById(expectedId);

            //then
            result.Data.Id.Should().Be(expectedId);
            result.Data.Id.Should().NotBe(expectedId + 1);
        }
    }
}
