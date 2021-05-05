using Microsoft.EntityFrameworkCore.Storage;
using SampleBank.Core.Abstractions.Persistence;

namespace SampleBank.Persistence.EF
{
    public class EfUnitOfWork : IUnitOfWork
    {
        //TODO: uncommented after database provider (commented for in-memory EF)
        /*private readonly DbContext _dbContext;

        public EfUnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
            _transaction = _dbContext.Database.BeginTransaction();
        }
        */

        private IDbContextTransaction _transaction;

        public void BeginTransaction()
        {
            //TODO: uncommented after database provider (commented for in-memory EF)
            //_transaction = _dbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            if (_transaction == null)
            {
                return;
            }

            try
            {
                _transaction.Commit();
                _transaction.Dispose();
            }
            finally
            {
                _transaction = null;
            }
        }

        public void Rollback()
        {
            if (_transaction == null)
            {
                return;
            }
            _transaction.Rollback();
        }

        public void Dispose()
        {
            if (_transaction == null)
            {
                return;
            }
            _transaction.Dispose();
        }
    }
}