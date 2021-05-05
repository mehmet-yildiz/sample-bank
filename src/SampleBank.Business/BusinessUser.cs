using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;

namespace SampleBank.Business
{
    public class BusinessUser : BusinessBase<User>, IBusinessUser
    {
        public BusinessUser(IPersistenceBase<User> persistence, IUnitOfWork uow) : base(persistence, uow)
        {
        }
    }
}