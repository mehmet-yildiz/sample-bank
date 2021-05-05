using System;
using System.Collections.Generic;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;

namespace SampleBank.Business
{
    public class BusinessBase<TEntity> : IBusinessBase<TEntity> where TEntity : class, IEntityKey<int>, new()
    {
        private readonly IPersistenceBase<TEntity> _persistence;
        protected readonly IUnitOfWork Uow;
        public BusinessBase(IPersistenceBase<TEntity> persistence, IUnitOfWork uow)
        {
            _persistence = persistence;
            Uow = uow;
        }

        public TEntity Insert(TEntity obj)
        {
            return _persistence.Insert(obj);
        }
        public TEntity Update(TEntity obj)
        {
            return _persistence.Update(obj);
        }

        public void Delete(TEntity obj)
        {
            _persistence.Delete(obj);
        }

        public TEntity GetById(int id)
        {
            return _persistence.GetById(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _persistence.GetAll();
        }

        public bool TransactionalProcess(Action action)
        {
            try
            {
                Uow.BeginTransaction();
                action();
                Uow.Commit();
                return true;
            }
            catch
            {
                Uow.Rollback();
                throw;
            }
        }
    }
}