using System;
using System.Linq;
using System.Linq.Expressions;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;

namespace SampleBank.Persistence
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class, IEntityKey<int>, new()
    {
        protected readonly IRepository Repository;

        public RepositoryBase(IRepository persistence)
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

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            return Repository.Select<TEntity>(includes);
        }
    }
}
