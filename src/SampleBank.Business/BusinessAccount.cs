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
        private readonly IRepositoryBase<Customer> _persistenceCustomer;
        private readonly IRepositoryTransaction _persistenceTransaction;
        public BusinessAccount(IRepositoryBase<Account> persistence, IUnitOfWork uow, ILogger logger, IRepositoryBase<Customer> persistenceCustomer, IRepositoryTransaction persistenceTransaction) : base(persistence, uow, logger)
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

                Repository.Insert(account);

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