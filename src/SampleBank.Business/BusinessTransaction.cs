using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;

namespace SampleBank.Business
{
    public class BusinessTransaction : BusinessBase<Transaction>, IBusinessTransaction
    {
        public BusinessTransaction(IPersistenceBase<Transaction> persistence, IUnitOfWork uow) : base(persistence, uow)
        {
        }
    }
}