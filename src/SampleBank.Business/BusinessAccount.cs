using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;

namespace SampleBank.Business
{
    public class BusinessAccount : BusinessBase<Account>, IBusinessAccount
    {
        public BusinessAccount(IPersistenceBase<Account> persistence, IUnitOfWork uow) : base(persistence, uow)
        {
        }
    }
}