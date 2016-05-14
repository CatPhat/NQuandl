using System;
using System.Data;
using System.Linq.Expressions;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Services.Extensions;

namespace NQuandl.Npgsql.Domain.Queries
{
    public class DataRecordsObservableByEntity<TEntity> : BaseEntitiesQuery<TEntity>,
        IDefineQuery<IObservable<IDataRecord>>
        where TEntity : DbEntity
    {
        public DataRecordsObservableByEntity() {}

        public DataRecordsObservableByEntity(Expression<Func<TEntity, object>> whereColumn, int query)
            : base(whereColumn, query) {}

        public DataRecordsObservableByEntity(Expression<Func<TEntity, object>> whereColumn, string query)
            : base(whereColumn, query) {}
    }

    public class HandleDataRecordsObservableByEntity<TEntity> :
        IHandleQuery<DataRecordsObservableByEntity<TEntity>, IObservable<IDataRecord>> where TEntity : DbEntity
    {
        private readonly IDbContext _dbContext;
        private readonly IEntityMetadataCache<TEntity> _metadata;

        public HandleDataRecordsObservableByEntity([NotNull] IEntityMetadataCache<TEntity> metadata, [NotNull] IDbContext dbContext)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (dbContext == null)
                throw new ArgumentNullException(nameof(dbContext));
            _metadata = metadata;
            _dbContext = dbContext;
        }

        public IObservable<IDataRecord> Handle(DataRecordsObservableByEntity<TEntity> query)
        {
            var dataRecordsQuery = _metadata.CreateDataRecordsQuery(query);
            return _dbContext.GetObservable(dataRecordsQuery);
        }
    }
}