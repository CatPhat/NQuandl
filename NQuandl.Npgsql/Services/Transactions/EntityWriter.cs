//using System;
//using System.Threading.Tasks;
//using JetBrains.Annotations;
//using NQuandl.Npgsql.Api;
//using NQuandl.Npgsql.Api.Entities;
//using NQuandl.Npgsql.Api.Transactions;

//namespace NQuandl.Npgsql.Services.Transactions
//{
//    public class EntityWriter<TEntity> : IWriteEntities<TEntity> where TEntity : DbEntity
//    {
//        private readonly IDb _db;
//        private readonly IEntitySqlMapper<TEntity> _sql;

//        public EntityWriter([NotNull] IEntitySqlMapper<TEntity> sql, [NotNull] IDb db)
//        {
//            if (sql == null)
//                throw new ArgumentNullException(nameof(sql));
//            if (db == null)
//                throw new ArgumentNullException(nameof(db));

//            _sql = sql;
//            _db = db;
//        }


//        public async Task BulkWriteEntities(IObservable<TEntity> entities)
//        {
//            var insertData = _sql.GetBulkInsertData(entities);
//            await _db.BulkWriteData(insertData.SqlStatement, insertData.DbDatasObservable);
//        }

//        public async Task WriteEntity(TEntity entity)
//        {
//            var insertData = _sql.GetInsertData(entity);
//            await _db.ExecuteCommandAsync(insertData.SqlStatement, insertData.DbDatas);
//        }
//    }
//}