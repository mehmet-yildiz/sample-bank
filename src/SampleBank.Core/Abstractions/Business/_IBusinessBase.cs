using System;
using System.Collections.Generic;
using SampleBank.Core.Model;

namespace SampleBank.Core.Abstractions.Business
{
    public interface IBusinessBase<TEntity> where TEntity : class
    {
        ServiceResponse<TEntity> Insert(TEntity obj);
        ServiceResponse<TEntity> Update(TEntity obj);
        ServiceResponse<bool> Delete(TEntity obj);
        ServiceResponse<TEntity> GetById(int id);
        ServiceResponse<IEnumerable<TEntity>> GetAll();
        ServiceResponse<bool> TransactionalProcess(Action action);
    }
}