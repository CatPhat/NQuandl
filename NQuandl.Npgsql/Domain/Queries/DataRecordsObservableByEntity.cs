using System;
using System.Data;
using System.Linq.Expressions;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Mappers;
using NQuandl.Npgsql.Api.Transactions;

namespace NQuandl.Npgsql.Domain.Queries
{
    public class DataRecordsObservableByEntity<TEntity> : BaseEntitiesQuery<TEntity>,
        IDefineQuery<IObservable<IDataRecord>>
        where TEntity : DbEntity
    {       
    }

    public class HandleDataRecordsObservableByEntity<TEntity> :
        IHandleQuery<DataRecordsObservableByEntity<TEntity>, IObservable<IDataRecord>> where TEntity : DbEntity
    {
        private readonly IEntityObjectMapper<TEntity> _objectMapper;
        private readonly IExecuteQueries _queries;

        public HandleDataRecordsObservableByEntity([NotNull] IEntityObjectMapper<TEntity> objectMapper,
            [NotNull] IExecuteQueries queries)
        {
            if (objectMapper == null)
                throw new ArgumentNullException(nameof(objectMapper));
            if (queries == null)
                throw new ArgumentNullException(nameof(queries));

            _objectMapper = objectMapper;
            _queries = queries;
        }

        public IObservable<IDataRecord> Handle(DataRecordsObservableByEntity<TEntity> query)
        {
            var readersQuery = _objectMapper.GetDataRecordsObservableQuery(query);
            return _queries.Execute(readersQuery);
        }
    }
}