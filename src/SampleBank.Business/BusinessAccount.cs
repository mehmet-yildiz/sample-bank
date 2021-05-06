using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Abstractions.Logging;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;
using SampleBank.Core.Model;
using SampleBank.Core.Model.Account;

namespace SampleBank.Business
{
    public class BusinessAccount : BusinessBase<Account>, IBusinessAccount
    {
        private readonly IPersistenceBase<Customer> _persistenceCustomer;
        private readonly IPersistenceBase<Transaction> _persistenceTransaction;
        public BusinessAccount(IPersistenceBase<Account> persistence, IUnitOfWork uow, ILogger logger, IPersistenceBase<Customer> persistenceCustomer, IPersistenceBase<Transaction> persistenceTransaction) : base(persistence, uow, logger)
        {
            _persistenceCustomer = persistenceCustomer;
            _persistenceTransaction = persistenceTransaction;
        }

        ServiceResponse<bool> IBusinessAccount.OpenAccount(OpenAccountModel model)
        {
            var response = new ServiceResponse<bool> { Data = false };

            TransactionalProcess(() =>
            {
                var customer = _persistenceCustomer.GetById(model.CustomerId);
                if (customer == null)
                {
                    Logger.LogError("CustomerNotFound");
                    response.ErrorMessage = "CustomerNotFound";
                }

                var account = new Account { CustomerId = model.CustomerId };

                Persistence.Insert(account);

                response.Data = true;
            });

            return response;
        }
    }
}