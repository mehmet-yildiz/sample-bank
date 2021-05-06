using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;

namespace SampleBank.Persistence
{
    public class RepositoryCustomer : RepositoryBase<Customer>, IRepositoryCustomer
    {
        public RepositoryCustomer(IRepository persistence) : base(persistence)
        {

        }
    }
}