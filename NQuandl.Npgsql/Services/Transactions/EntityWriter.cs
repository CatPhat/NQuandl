using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Api.Transactions;

namespace NQuandl.Npgsql.Services.Transactions
{
    public class EntityWriter<TEntity> : IWriteEntities<TEntity> where TEntity : DbEntity
    {
        private readonly IExecuteRawSql _db;
        private readonly IEntityMetadata<TEntity> _metadata;
        private readonly IEntitySqlMapper<TEntity> _sql;

        public EntityWriter([NotNull] IEntitySqlMapper<TEntity> sql, [NotNull] IEntityMetadata<TEntity> metadata,
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

        public async Task WriteEntity(TEntity entity)
        {
            var parameters = GetParameters(entity);
            var insertStatement = _sql.GetInsertSql(parameters);
            await _db.ExecuteCommandAsync(insertStatement, parameters);
        }

        //todo am i mixing concerns with depending on NpgsqlParameters?
        private NpgsqlParameter[] GetParameters(TEntity entity)
        {
            var datas = _metadata.GetDbDatas(entity);
            return datas.Select(dbData => new NpgsqlParameter(dbData.ColumnName, dbData.DbType)
            {
                Value = dbData.Data
            }).ToArray();
        }
       

        public async Task BulkWriteEntities(IObservable<TEntity> entities)
        {
            await _db.BulkWriteData(_sql.BulkInsertSql(), GetBulkImportDatas(entities));
        }

        private IObservable<List<DbData>> GetBulkImportDatas(IObservable<TEntity> entities)
        {
            return Observable.Create<List<DbData>>(observer =>
                entities.Subscribe(entity => observer.OnNext(_metadata.GetDbDatas(entity)), 
                onCompleted: observer.OnCompleted, 
                onError: ex => {throw new Exception(ex.Message);}));
        }


    }
}