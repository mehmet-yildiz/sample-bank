using System.Linq;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;

namespace SampleBank.Persistence
{
    public class PersistenceBase<TEntity> : IPersistenceBase<TEntity> where TEntity : class, IEntityKey<int>, new()
    {
        protected readonly IRepository Repository;

        public PersistenceBase(IRepository persistence)
        {
            Repository = persistence;
        }

        public TEntity Insert(TEntity obj)
        {
            return Repository.Insert(obj);
        }
        public TEntity Update(TEntity obj)
        {
            return Repository.Update(obj);
        }
        public void Delete(TEntity obj)
        {
            Repository.Delete(obj);
        }

        public TEntity GetById(int id)
        {
            return Repository.Select<TEntity>().FirstOrDefault(x => x.Id == id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Repository.Select<TEntity>();
        }
    }
}
