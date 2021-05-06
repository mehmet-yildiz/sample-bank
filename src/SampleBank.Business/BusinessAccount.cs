
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
        private readonly IPersistenceTransaction _persistenceTransaction;
        public BusinessAccount(IPersistenceBase<Account> persistence, IUnitOfWork uow, ILogger logger, IPersistenceBase<Customer> persistenceCustomer, IPersistenceTransaction persistenceTransaction) : base(persistence, uow, logger)
        {
            _persistenceCustomer = persistenceCustomer;
            _persistenceTransaction = persistenceTransaction;
        }

        /// <summary>
        /// Opens the account connected to user with provided CustomerId and decide sending a transaction by initialCredit parameter value.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ServiceResponse<bool> OpenAccount(OpenAccountModel model)
        {
            var errorMessage = "";
            var data = false;
            var serviceResponse = TransactionalProcess(() =>
            {
                var customer = _persistenceCustomer.GetById(model.CustomerId);
                if (customer == null)
                {
                    Logger.LogError("CustomerNotFound");
                    errorMessage = "CustomerNotFound";
                }

                var account = new Account { CustomerId = model.CustomerId, InitialCredit = model.InitialCredit};

                Persistence.Insert(account);

                //refactor
                if (model.InitialCredit > 0)
                {
                    _persistenceTransaction.CreateNewCreditRequest(account.Id, model.InitialCredit);
                }

                data = true;
            });

            serviceResponse.Data = data;
            serviceResponse.ErrorMessage = errorMessage;

            return serviceResponse;
        }
    }
}