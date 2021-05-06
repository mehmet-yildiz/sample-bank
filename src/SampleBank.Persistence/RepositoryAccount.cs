using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;

namespace SampleBank.Persistence
{
    public class RepositoryAccount : RepositoryBase<Account>, IRepositoryAccount
    {
        public RepositoryAccount(IRepository persistence) : base(persistence)
        {
        }
    }
}