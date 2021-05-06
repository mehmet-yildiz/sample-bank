using System;
using System.Collections.Generic;
using SampleBank.Core.Abstractions.Business;
using SampleBank.Core.Abstractions.Logging;
using SampleBank.Core.Abstractions.Persistence;
using SampleBank.Core.Entity;
using SampleBank.Core.Model;

namespace SampleBank.Business
{
    public class BusinessBase<TEntity> : IBusinessBase<TEntity> where TEntity : class, IEntityKey<int>, new()
    {
        protected readonly IPersistenceBase<TEntity> Persistence;
        protected readonly IUnitOfWork Uow;
        protected readonly ILogger Logger;

        public BusinessBase(IPersistenceBase<TEntity> persistence, IUnitOfWork uow, ILogger logger)
        {
            Persistence = persistence;
            Uow = uow;
            Logger = logger;
        }

        public ServiceResponse<TEntity> Insert(TEntity obj)
        {
            var response = new ServiceResponse<TEntity>();
            try
            {
                response.Data = Persistence.Insert(obj);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                response.ErrorMessage = "GeneralInsertError";
            }
            return response;
        }
        public ServiceResponse<TEntity> Update(TEntity obj)
        {
            var response = new ServiceResponse<TEntity>();
            try
            {
                response.Data = Persistence.Update(obj);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                response.ErrorMessage = "GeneralUpdateError";
            }
            return response;
        }

        public ServiceResponse<bool> Delete(TEntity obj)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                Persistence.Delete(obj);
                response.Data = true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                response.ErrorMessage = "GeneralDeleteError";
            }
            return response;
        }

        public ServiceResponse<TEntity> GetById(int id)
        {
            var response = new ServiceResponse<TEntity>();
            try
            {
                response.Data = Persistence.GetById(id);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                response.ErrorMessage = "GeneralGetByIdError";
            }
            return response;
        }

        public ServiceResponse<IEnumerable<TEntity>> GetAll()
        {
            var response = new ServiceResponse<IEnumerable<TEntity>>();
            try
            {
                response.Data = Persistence.GetAll();
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                response.ErrorMessage = "GeneralGetAllError";
            }
            return response;
        }

        public ServiceResponse<bool> TransactionalProcess(Action action)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                Uow.BeginTransaction();
                action();
                Uow.Commit();
                response.Data = true;
            }
            catch (Exception ex)
            {
                Uow.Rollback();
                Logger.LogError(ex);
                response.ErrorMessage = "GeneralTransactionalProcessError";
            }
            return response;
        }
    }
}