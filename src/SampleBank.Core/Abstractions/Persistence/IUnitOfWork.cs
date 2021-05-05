using System;

namespace SampleBank.Core.Abstractions.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();

        void Commit();

        void Rollback();
    }
}