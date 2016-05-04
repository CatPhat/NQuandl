using System;
using System.Collections.Generic;
using System.Data;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Queries;
using NQuandl.Npgsql.Services.Mappers;

namespace NQuandl.Npgsql.Services.Transactions
{
    public class EntityReader<TEntity> : IReadEntities<TEntity> where TEntity : DbEntity
    {
        private readonly IExecuteRawSql _db;
        private readonly IEntityMetadata<TEntity> _metadata;
        private readonly IEntitySqlMapper<TEntity> _sql;


        public EntityReader([NotNull] IEntitySqlMapper<TEntity> sql, [NotNull] IEntityMetadata<TEntity> metadata,
            [NotNull] IExecuteRawSql db)
        {
            if (sql == null)
                throw new ArgumentNullException(nameof(sql));
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            if (db == null)
                throw new ArgumentNullException(nameof(db));
            _sql = sql;
            _metadata = metadata;
            _db = db;
        }

        public async Task<TEntity> GetAsync(EntitiesReaderQuery<TEntity> query)
        {
            var result = await GetRecords(query).FirstOrDefaultAsync();
            return _metadata.CreateEntity(result);
        }


        public IObservable<TEntity> GetObservable(EntitiesReaderQuery<TEntity> query)
        {
            var result = GetRecords(query);

            return Observable.Create<TEntity>(
                obs => result.Subscribe(
                    record => obs.OnNext(_metadata.CreateEntity(record)), onCompleted: obs.OnCompleted, onError:
                        exception => { throw new Exception(exception.Message); }));
        }

     

        private IObservable<IDataRecord> GetRecords(EntitiesReaderQuery<TEntity> query)
        {
            var queryString = _sql.GetSelectSqlBy(query);
            return _db.ExecuteQueryAsync(queryString);
        }
    }
}