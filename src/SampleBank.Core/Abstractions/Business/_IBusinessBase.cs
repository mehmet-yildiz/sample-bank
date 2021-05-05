using System.Collections.Generic;

namespace SampleBank.Core.Abstractions.Business
{
    public interface IBusinessBase<TEntity> where TEntity : class
    {
        TEntity Insert(TEntity obj);
        TEntity Update(TEntity obj);
        void Delete(TEntity obj);
        TEntity GetById(int id);
        IEnumerable<TEntity> GetAll();
    }
}