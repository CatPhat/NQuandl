using System.Collections.Generic;
using Npgsql;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Domain.Queries;
using NQuandl.Npgsql.Services.Transactions;

namespace NQuandl.Npgsql.Api.Transactions
{
    public interface IEntitySqlMapper<TEntity> where TEntity : DbEntity
    {
        string BulkInsertSql();
        string GetSelectSqlBy(EntitiesReaderQuery<TEntity> query);
        InsertData GetInsertData(TEntity entity);
        IEnumerable<DbData> GetDbDatas(TEntity entity);
    }
}