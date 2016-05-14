using System;
using System.Data;
using System.Linq.Expressions;
using System.Reactive.Linq;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Domain.Queries
{
    public class EntitiesObservableBy<TEntity> : BaseEntitiesQuery<TEntity>, IDefineQuery<IObservable<TEntity>>
        where TEntity : DbEntity
    {
        public EntitiesObservableBy() {}

        public EntitiesObservableBy(Expression<Func<TEntity, object>> whereColumn, int query)
            : base(whereColumn, query) {}

        public EntitiesObservableBy(Expression<Func<TEntity, object>> whereColumn, string query)
            : base(whereColumn, query) {}
    }

    public class HandleEntitiesObservableBy<TEntity> : IHandleQuery<EntitiesObservableBy<TEntity>, IObservable<TEntity>>
        where TEntity : DbEntity
    {
        private readonly IDbContext _dbContext;
        private readonly IEntityMetadataCache<TEntity> _metadata;

        public HandleEntitiesObservableBy([NotNull] IEntityMetadataCache<TEntity> metadata, [NotNull] IDbContext dbContext)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));
            _metadata = metadata;
            _dbContext = dbContext;
        }

        public IObservable<TEntity> Handle(EntitiesObservableBy<TEntity> query)
        {
            var dataRecordsQuery = _metadata.CreateDataRecordsQuery(query);
            var observable = _dbContext.GetObservable(dataRecordsQuery);
            return GetEntityObservable(observable);
        }

        private IObservable<TEntity> GetEntityObservable(IObservable<IDataRecord> records)
        {
            return Observable.Create<TEntity>(
                obs => records.Subscribe(
                    record => obs.OnNext(CreateEntity(record)), onCompleted: obs.OnCompleted, onError:
                        exception => { throw new Exception(exception.Message); }));
        }

        private TEntity CreateEntity(IDataRecord record)
        {
            var entity = (TEntity) Activator.CreateInstance(typeof(TEntity), new object[] {});
            var properties = _metadata.GetPropertyInfos();

            foreach (var propertyInfo in properties)
            {
                var columnIndex = _metadata.GetColumnIndex(propertyInfo.Name);
                var recordValue = record[columnIndex];
                if (recordValue != DBNull.Value)
                {
                    propertyInfo.SetValue(entity, recordValue);
                }
            }
            return entity;
        }
    }
}