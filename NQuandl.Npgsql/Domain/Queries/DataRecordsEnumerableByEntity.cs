using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Mappers;
using NQuandl.Npgsql.Api.Transactions;

namespace NQuandl.Npgsql.Domain.Queries
{
    public class DataRecordsEnumerableByEntity<TEntity> : BaseEntitiesQuery<TEntity>,
        IDefineQuery<IEnumerable<IDataRecord>>
        where TEntity : DbEntity
    {
        public DataRecordsEnumerableByEntity()
        {
        }

        public DataRecordsEnumerableByEntity(Expression<Func<TEntity, object>> whereColumn, int query) : base(whereColumn, query)
        {
        }

        public DataRecordsEnumerableByEntity(Expression<Func<TEntity, object>> whereColumn, string query) : base(whereColumn, query)
        {
        }
    }

    public class HandleDataRecordsEnumerableByEntity<TEntity> :
        IHandleQuery<DataRecordsEnumerableByEntity<TEntity>, IEnumerable<IDataRecord>> where TEntity : DbEntity
    {
        private readonly IEntityObjectMapper<TEntity> _objectMapper;
        private readonly IExecuteQueries _queries;

        public HandleDataRecordsEnumerableByEntity([NotNull] IEntityObjectMapper<TEntity> objectMapper,
            [NotNull] IExecuteQueries queries)
        {
            if (objectMapper == null)
                throw new ArgumentNullException(nameof(objectMapper));
            if (queries == null)
                throw new ArgumentNullException(nameof(queries));

            _objectMapper = objectMapper;
            _queries = queries;
        }

        public IEnumerable<IDataRecord> Handle(DataRecordsEnumerableByEntity<TEntity> query)
        {
            var readersQuery = _objectMapper.GetDataRecordsQuery<DataRecordsEnumerableByEntity<TEntity>,DataRecordsEnumerableBy>(query);
            return _queries.Execute(readersQuery);
        }
    }
}