using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Abstractions.Logging;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;

namespace SampleBank.Business
{
    public class BusinessCustomer : BusinessBase<Customer>, IBusinessCustomer
    {
        public BusinessCustomer(IPersistenceBase<Customer> persistence, IUnitOfWork uow, ILogger logger) : base(persistence, uow, logger)
        {
        }
    }
}