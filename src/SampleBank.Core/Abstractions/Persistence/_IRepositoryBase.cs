using System;
using System.Linq;
using System.Linq.Expressions;

namespace SampleBank.Core.Abstractions.Persistence
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
        TEntity Insert(TEntity obj);
        TEntity Update(TEntity obj);
        void Delete(TEntity obj);
    }
}