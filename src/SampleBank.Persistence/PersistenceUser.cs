using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;

namespace SampleBank.Persistence
{
    public class PersistenceUser : PersistenceBase<User>, IPersistenceUser
    {
        public PersistenceUser(IRepository persistence) : base(persistence)
        {
        }
    }
}