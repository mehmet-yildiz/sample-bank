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
        protected readonly IRepositoryBase<TEntity> Repository;
        protected readonly IUnitOfWork Uow;
        protected readonly ILogger Logger;

        public BusinessBase(IRepositoryBase<TEntity> persistence, IUnitOfWork uow, ILogger logger)
        {
            Repository = persistence;
            Uow = uow;
            Logger = logger;
        }

        public ServiceResponse<TEntity> Insert(TEntity obj)
        {
            var response = new ServiceResponse<TEntity>();
            try
            {
                response.Data = Repository.Insert(obj);
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
                response.Data = Repository.Update(obj);
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
                Repository.Delete(obj);
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
                response.Data = Repository.GetById(id);
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
                response.Data = Repository.GetAll();
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