using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SampleBank.Core.Abstractions.Persistence;

namespace SampleBank.Persistence.EF
{
    public class EfRepository : IRepository
    {
        private readonly DbContext _context;

        public EfRepository(DbContext context)
        {
            _context = context;
        }

        private DbSet<T> Set<T>() where T : class, new()
        {
            return _context.Set<T>();
        }

        public T Insert<T>(T entity) where T : class, new()
        {
            SetState(entity, EntityState.Added);
            return entity;
        }

        public T Update<T>(T entity) where T : class, new()
        {
            SetState(entity, EntityState.Modified);
            return entity;
        }

        public void Delete<T>(T entity) where T : class, new()
        {
            SetState(entity, EntityState.Deleted);
        }

        public IQueryable<T> Select<T>() where T : class, new()
        {
            return Set<T>().AsNoTracking();
        }
        
        public IList<T> Query<T>(string spNameOrSql, IDictionary<string, object> args = null, bool rawSql = false) where T : class, new()
        {
            throw new NotImplementedException();
        }

        public void NonQuery(string spNameOrSql, IDictionary<string, object> args = null, bool rawSql = false)
        {
            throw new NotImplementedException();
        }


        private void SetState<T>(T entity, EntityState state) where T : class, new()
        {
            var entry = _context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Set<T>().Attach(entity);
            }
            entry.State = state;

            _context.SaveChanges();
        }
    }
}