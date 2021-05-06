using SampleBank.Core.Entity;

namespace SampleBank.Core.Abstractions.Persistence
{
    public interface IPersistenceTransaction : IPersistenceBase<Transaction>
    {
        void CreateNewCreditRequest(int accountId, decimal initialCredit);
    }
}