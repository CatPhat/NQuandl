using System;
using System.Collections.Generic;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Entities;


namespace NQuandl.Npgsql.Api.Transactions
{
    public interface ISqlMapper
    {
        string GetBulkInsertSql(string tableName, string[] columnNames);
    }

    public interface IEntitySqlMapper<TEntity> where TEntity : DbEntity
    {
        string GetBulkInsertSql();
    }
}