using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
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
            [NotNull] IExecuteRawSql db
            )
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

        public async Task BulkWriteEntities(IObservable<TEntity> entities)
        {
            await _db.BulkWriteData(_sql.BulkInsertSql(), GetBulkImportDatas(entities));
        }

        private IObservable<IObservable<BulkImportData>> GetBulkImportDatas(IObservable<TEntity> entities)
        {
            return Observable.Create<IObservable<BulkImportData>>(observer =>
                entities.Subscribe(entity => observer.OnNext(GetBulkImportData(entity))));
        }

        private IObservable<BulkImportData> GetBulkImportData(TEntity entityWithData)
        {
            return (from keyValue in _metadata.GetProperyNameDbMetadata().OrderBy(x => x.Value.ColumnIndex)
                let data = _metadata.GetEntityValueByPropertyName(entityWithData, keyValue.Key)
                where data != null
                select new BulkImportData
                {
                    Data = data,
                    DbType = keyValue.Value.DbType
                }).ToObservable();
        }
    }
}