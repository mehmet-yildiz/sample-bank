using System.Linq;

namespace SampleBank.Core.Abstractions.Persistence
{
    public interface IPersistenceBase<TEntity> where TEntity : class
    {
        TEntity GetById(int id);
        IQueryable<TEntity> GetAll();
        TEntity Insert(TEntity obj);
        TEntity Update(TEntity obj);
        void Delete(TEntity obj);
    }
}