using System.Collections.Generic;
using NQuandl.Npgsql.Api.DTO;

namespace NQuandl.Npgsql.Api.Transactions
{
    public interface ISqlMapper
    {
        string GetSelectSqlBy(DataRecordsQuery query);
        string GetBulkInsertSql(string tableName, IEnumerable<DbInsertData> dbInsertDatas);
        string GetInsertSql(string tableName, IEnumerable<DbInsertData> dbDatas);
    }
}