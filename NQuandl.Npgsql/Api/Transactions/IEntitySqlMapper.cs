using System;
using System.Collections.Generic;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Entities;


namespace NQuandl.Npgsql.Api.Transactions
{
    public interface IEntitySqlMapper<TEntity> where TEntity : DbEntity
    {
        string GetBulkInsertSql();
        string GetInsertSql(IEnumerable<DbInsertData> dbDatas);
    }
}