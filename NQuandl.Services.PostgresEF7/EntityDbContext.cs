using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using NQuandl.Api.Entities;
using NQuandl.Domain.Quandl.Entities;

namespace NQuandl.Services.PostgresEF7
{
    public class EntityDbContext : DbContext, IWriteEntities
    {
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FredGdp>().Property(b => b.DatabaseCode).IsRequired();
            modelBuilder.Entity<FredGdp>().Property(b => b.DatasetCode).IsRequired();
            modelBuilder.Entity<FredGdp>().Property(b => b.Date).IsRequired();
            modelBuilder.Entity<FredGdp>().Property(b => b.Value).IsRequired();
            base.OnModelCreating(modelBuilder);
        }

        #region Queries

        public IQueryable<TEntity> EagerLoad<TEntity>(IQueryable<TEntity> query,
            Expression<Func<TEntity, object>> expression) where TEntity : Entity
        {
            // Include will eager load data into the query
            if (query != null && expression != null) query = query.Include(expression);
            return query;
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : Entity
        {
            // AsNoTracking returns entities that are not attached to the DbContext
            return new EntitySet<TEntity>(Set<TEntity>().AsNoTracking(), this);
        }

        #endregion

        #region Commands

        public TEntity Get<TEntity>(object firstKeyValue, params object[] otherKeyValues) where TEntity : Entity
        {
            if (firstKeyValue == null) throw new ArgumentNullException("firstKeyValue");
            var keyValues = new List<object> {firstKeyValue};
            if (otherKeyValues != null) keyValues.AddRange(otherKeyValues);
            return Set<TEntity>().FirstOrDefault(x => x.Equals(keyValues.ToArray()));
        }

        public Task<TEntity> GetAsync<TEntity>(object firstKeyValue, params object[] otherKeyValues)
            where TEntity : Entity
        {
            if (firstKeyValue == null) throw new ArgumentNullException("firstKeyValue");
            var keyValues = new List<object> {firstKeyValue};
            if (otherKeyValues != null) keyValues.AddRange(otherKeyValues);
            return Set<TEntity>().FirstOrDefaultAsync(x => x.Equals(keyValues.ToArray()));
        }

        public IQueryable<TEntity> Get<TEntity>() where TEntity : Entity
        {
            return new EntitySet<TEntity>(Set<TEntity>(), this);
        }

        public void Create<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (Entry(entity).State == EntityState.Detached) Set<TEntity>().Add(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : Entity
        {
            var entry = Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete<TEntity>(TEntity entity) where TEntity : Entity
        {
            if (Entry(entity).State != EntityState.Deleted)
                Set<TEntity>().Remove(entity);
        }

        #endregion
    }
}