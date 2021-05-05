using System.Collections.Generic;
using System.Linq;

namespace SampleBank.Core.Abstractions.Persistence
{
    public interface IRepository
    {
        T Insert<T>(T entity) where T : class, new();
        T Update<T>(T entity) where T : class, new();
        void Delete<T>(T entity) where T : class, new();
        IQueryable<T> Select<T>() where T : class, new();

        IList<T> Query<T>(string spNameOrSql, IDictionary<string, object> args = null, bool rawSql = false) where T : class, new();
        void NonQuery(string spNameOrSql, IDictionary<string, object> args = null, bool rawSql = false);
    }
}
