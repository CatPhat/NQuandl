using System;
using System.Collections.Generic;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Entities;


namespace NQuandl.Npgsql.Api.Transactions
{
    public interface ISqlMapper
    {
        string GetSelectSqlBy(ReaderQuery query);
        string GetBulkInsertSql(BulkInsertCommand command);
        string GetInsertSql(InsertDataCommand command);
    }

    public interface IEntitySqlMapper<TEntity> where TEntity : DbEntity
    {
        string GetSelectSqlBy(ReaderQuery query);
        string GetBulkInsertSql(string tableName, string[] columnNames);
        string GetInsertSql(List<DbData> dbDatas, string tableName);
    }
}