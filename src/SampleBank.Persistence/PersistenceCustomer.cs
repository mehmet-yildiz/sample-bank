using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;

namespace SampleBank.Persistence
{
    public class PersistenceCustomer : PersistenceBase<Customer>, IPersistenceCustomer
    {
        public PersistenceCustomer(IRepository persistence) : base(persistence)
        {
        }
    }
}