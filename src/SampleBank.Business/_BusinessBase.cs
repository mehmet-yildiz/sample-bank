using System;
using System.Collections.Generic;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;

namespace SampleBank.Business
{
    public class BusinessBase<TEntity> : IBusinessBase<TEntity> where TEntity : class, IEntityKey<int>, new()
    {
        protected readonly IPersistenceBase<TEntity> Persistence;
        protected readonly IUnitOfWork Uow;
        public BusinessBase(IPersistenceBase<TEntity> persistence, IUnitOfWork uow)
        {
            Persistence = persistence;
            Uow = uow;
        }

        public TEntity Insert(TEntity obj)
        {
            return Persistence.Insert(obj);
        }
        public TEntity Update(TEntity obj)
        {
            return Persistence.Update(obj);
        }

        public void Delete(TEntity obj)
        {
            Persistence.Delete(obj);
        }

        public TEntity GetById(int id)
        {
            return Persistence.GetById(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Persistence.GetAll();
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