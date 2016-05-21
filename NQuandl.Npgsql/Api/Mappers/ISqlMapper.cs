using System.Collections.Generic;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Commands;

namespace NQuandl.Npgsql.Api.Mappers
{
    public interface ISqlMapper
    {
        string GetSelectSqlBy(DataRecordsQuery query);
        string GetBulkInsertSql(string tableName, IEnumerable<DbInsertData> dbInsertDatas);
        string GetInsertSql(string tableName, IEnumerable<DbInsertData> dbDatas);
        string GetDeleteRowSql(DeleteCommand command);
    }
}