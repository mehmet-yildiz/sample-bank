using SampleBank.Core.Entity;

namespace SampleBank.Core.Abstractions.Persistence
{
    public interface IRepositoryTransaction : IRepositoryBase<Transaction>
    {
        void CreateNewCreditRequest(int accountId, decimal initialCredit);
    }
}