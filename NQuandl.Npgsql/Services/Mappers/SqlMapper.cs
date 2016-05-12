using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Transactions;

namespace NQuandl.Npgsql.Services.Mappers
{
    public class SqlMapper : ISqlMapper
    {
        public string GetSelectSqlBy(DataRecordsQuery query)
        {
            var queryString =
                new StringBuilder($"SELECT {GetColumnNamesString(query.ColumnNames)} FROM {query.TableName}");

            if (!string.IsNullOrEmpty(query.WhereColumn))
            {
                if (!query.QueryByInt.HasValue && string.IsNullOrEmpty(query.QueryByString))
                {
                    throw new Exception("missing value for where clause.");
                }
                var whereValue = query.QueryByInt.HasValue ? $"{query.QueryByInt.Value}" : $"'{query.QueryByString}'";
                queryString.Append($" WHERE {query.WhereColumn} = {whereValue}");
            }

            if (!string.IsNullOrEmpty(query.OrderByColumn))
            {
                queryString.Append($" ORDER BY {query.OrderByColumn}");
            }

            if (query.Limit.HasValue)
            {
                queryString.Append($" LIMIT {query.Limit.Value}");
            }

            if (query.Offset.HasValue)
            {
                queryString.Append($" OFFSET {query.Offset.Value}");
            }

            return queryString.ToString();
        }

        public string GetBulkInsertSql(string tableName, IEnumerable<DbInsertData> dbInsertDatas)
        {
            var dbDatas = dbInsertDatas as IList<DbInsertData> ?? dbInsertDatas.ToList();
            var columnNames = GetColumnNamesString(dbDatas.Select(x => x.ColumnName).ToArray());
            return
               $"COPY {tableName} ({columnNames}) FROM STDIN (FORMAT BINARY)";
        }

        public string GetInsertSql(string tableName, IEnumerable<DbInsertData> dbDatas)
        {
            var dbInsertDatas = dbDatas as IList<DbInsertData> ?? dbDatas.ToList();
            var columnNames = GetColumnNamesString(dbInsertDatas.Select(x => x.ColumnName).ToArray());
            return
                $"INSERT INTO {tableName} ({columnNames}) " +
                $"VALUES ({string.Join(",", dbInsertDatas.Select(x => $":{x.ColumnName}"))});";
        }

        private static string GetColumnNamesString(string[] columnNames)
        {
            return string.Join(",", columnNames);
        }
    }
}