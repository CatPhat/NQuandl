using System;
using System.Data;
using System.Linq.Expressions;
using System.Reactive.Linq;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Mappers;
using NQuandl.Npgsql.Api.Transactions;

namespace NQuandl.Npgsql.Domain.Queries
{
    public class DataRecordsObservableBy<TEntity> : BaseEntitiesQuery<TEntity>, IDefineQuery<IObservable<IDataRecord>>
        where TEntity : DbEntity
    {
        public DataRecordsObservableBy() {}

        public DataRecordsObservableBy(Expression<Func<TEntity, object>> where, string query)
        {
            QueryByString = query;
            WhereColumn = where;
        }

        public DataRecordsObservableBy(Expression<Func<TEntity, object>> where, int query)
        {
            QueryByInt = query;
            WhereColumn = where;
        }
    }

    public class HandleDataRecordsObservableBy<TEntity> :
        IHandleQuery<DataRecordsObservableBy<TEntity>, IObservable<IDataRecord>> where TEntity : DbEntity
    {
        private readonly IDb _db;
        private readonly IEntityObjectMapper<TEntity> _objectMapper;

        private readonly ISqlMapper _sqlMapper;

        public HandleDataRecordsObservableBy([NotNull] ISqlMapper sqlMapper,
            [NotNull] IEntityObjectMapper<TEntity> objectMapper, [NotNull] IDb db)
        {
            if (sqlMapper == null)
                throw new ArgumentNullException(nameof(sqlMapper));
            if (objectMapper == null)
                throw new ArgumentNullException(nameof(objectMapper));
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            _sqlMapper = sqlMapper;
            _objectMapper = objectMapper;
            _db = db;
        }

        public IObservable<IDataRecord> Handle(DataRecordsObservableBy<TEntity> query)
        {
            var readersQuery = _objectMapper.GetReaderQuery(query);

            var sqlStatement = _sqlMapper.GetSelectSqlBy(readersQuery);
            return Observable.Create<IDataRecord>(async obs =>
            {
                using (var connection = _db.CreateConnection())
                using (var cmd = new NpgsqlCommand(sqlStatement, connection))
                {
                    await cmd.Connection.OpenAsync();
                    using (var reader = cmd.ExecuteReaderAsync())
                    {
                        var result = await reader;
                        while (await result.ReadAsync())
                        {
                            obs.OnNext(result);
                        }
                        obs.OnCompleted();
                    }
                    cmd.Connection.Close();
                }
            });
        }
    }
}