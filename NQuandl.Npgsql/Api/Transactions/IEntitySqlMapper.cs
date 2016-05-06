using System;
using System.Collections.Generic;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Services.Transactions;

namespace NQuandl.Npgsql.Api.Transactions
{
    public interface IEntitySqlMapper<TEntity> where TEntity : DbEntity
    {
     
        string GetSelectSqlBy(EntitiesReaderQuery<TEntity> query);
        InsertData GetInsertData(TEntity entity);
        List<DbData> GetDbDatas(TEntity entity, bool excludeIsStoreGenerated = false);
        BulkInsertData GetBulkInsertData(IObservable<TEntity> entities);
    }
}