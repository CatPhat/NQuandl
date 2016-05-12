using System;
using System.Collections.Generic;
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
    public class DataRecordsEnumerableByEntity<TEntity> : BaseEntitiesQuery<TEntity>,
        IDefineQuery<IEnumerable<IDataRecord>>
        where TEntity : DbEntity
    {
        public DataRecordsEnumerableByEntity() {}

        public DataRecordsEnumerableByEntity(Expression<Func<TEntity, object>> whereColumn, int query)
            : base(whereColumn, query) {}

        public DataRecordsEnumerableByEntity(Expression<Func<TEntity, object>> whereColumn, string query)
            : base(whereColumn, query) {}
    }

    public class HandleDataRecordsEnumerableByEntity<TEntity> :
        IHandleQuery<DataRecordsEnumerableByEntity<TEntity>, IEnumerable<IDataRecord>> where TEntity : DbEntity
    {
        private readonly IDb _db;
        private readonly IEntityMetadataCache<TEntity> _metadata;

        public HandleDataRecordsEnumerableByEntity([NotNull] IEntityMetadataCache<TEntity> metadata, [NotNull] IDb db)
        {
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            _metadata = metadata;
            _db = db;
        }

        public IEnumerable<IDataRecord> Handle(DataRecordsEnumerableByEntity<TEntity> query)
        {
            var dataRecordsQuery = _metadata.CreateDataRecordsQuery(query);
            return _db.GetEnumerable(dataRecordsQuery);
        }
    }
}