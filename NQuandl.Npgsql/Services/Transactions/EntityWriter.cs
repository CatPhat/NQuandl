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
        private readonly IDb _db;
        private readonly IEntitySqlMapper<TEntity> _sql;

        public EntityWriter([NotNull] IEntitySqlMapper<TEntity> sql, [NotNull] IDb db)
        {
            if (sql == null)
                throw new ArgumentNullException(nameof(sql));
            if (db == null)
                throw new ArgumentNullException(nameof(db));

            _sql = sql;
            _db = db;
        }

        public async Task WriteEntity(TEntity entity)
        {
            var insertData = _sql.GetInsertData(entity);
            
            await _db.ExecuteCommandAsync(insertData.SqlStatement, insertData.Parameters);
        }

   
       

        public async Task BulkWriteEntities(IObservable<TEntity> entities)
        {
            await _db.BulkWriteData(_sql.BulkInsertSql(), GetBulkImportDatas(entities));
        }

        private IObservable<List<DbData>> GetBulkImportDatas(IObservable<TEntity> entities)
        {
            return Observable.Create<List<DbData>>(observer =>
                entities.Subscribe(entity => observer.OnNext(_sql.GetDbDatas(entity).ToList()), 
                onCompleted: observer.OnCompleted, 
                onError: ex => {throw new Exception(ex.Message);}));
        }


    }
}