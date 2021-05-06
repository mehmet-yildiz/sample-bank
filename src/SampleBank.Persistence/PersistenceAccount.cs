using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;

namespace SampleBank.Persistence
{
    public class PersistenceAccount : PersistenceBase<Account>, IPersistenceAccount
    {
        public PersistenceAccount(IRepository persistence) : base(persistence)
        {
        }
    }
}