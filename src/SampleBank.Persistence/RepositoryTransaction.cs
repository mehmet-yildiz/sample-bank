using System;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;
using SampleBank.Core.Enums;

namespace SampleBank.Persistence
{
    public class RepositoryTransaction : RepositoryBase<Transaction>, IRepositoryTransaction
    {
        public RepositoryTransaction(IRepository persistence) : base(persistence)
        {
        }

        public void CreateNewCreditRequest(int accountId, decimal initialCredit)
        {
            var newTransaction = new Transaction
            {
                AccountId = accountId, ProcessDate = DateTime.UtcNow,
                TransactionProcess = ProcessesType.CreditRequest
            };

            Repository.Insert(newTransaction);
        }
    }
}