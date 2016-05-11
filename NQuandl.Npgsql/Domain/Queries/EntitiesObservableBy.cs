using System;
using System.Linq.Expressions;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Mappers;
using NQuandl.Npgsql.Api.Transactions;

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
        private readonly IExecuteQueries _queries;
        private readonly IEntityObjectMapper<TEntity> _objectMapper;

        public HandleEntitiesObservableBy([NotNull] IExecuteQueries queries,
            [NotNull] IEntityObjectMapper<TEntity> objectMapper)
        {
            if (queries == null)
                throw new ArgumentNullException(nameof(queries));
            if (objectMapper == null)
                throw new ArgumentNullException(nameof(objectMapper));
            _queries = queries;
            _objectMapper = objectMapper;
        }

        public IObservable<TEntity> Handle(EntitiesObservableBy<TEntity> query)
        {
           var dataRecordQuery = _objectMapper.GetDataRecordsQuery<EntitiesObservableBy<TEntity>, DataRecordsObservableBy>(query);
           var result = _queries.Execute(dataRecordQuery);
           return _objectMapper.GetEntityObservable(result);
        }
    }
}