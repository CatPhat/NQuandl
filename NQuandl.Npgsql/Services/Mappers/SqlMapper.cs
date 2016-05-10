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
        public string GetSelectSqlBy<TQuery>(TQuery query) where TQuery : BaseDataRecordsQuery
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

        public string GetBulkInsertSql(string tableName, string[] columnNames)
        {
            var columnNamesString = GetColumnNamesString(columnNames);
            return
                $"COPY {tableName} ({columnNamesString}) FROM STDIN (FORMAT BINARY)";
        }

        public string GetInsertSql(string tableName, string[] columnNames, IEnumerable<DbImportData> dbDatas)
        {
            return
                $"INSERT INTO {tableName} ({columnNames}) " +
                $"VALUES ({string.Join(",", dbDatas.Select(x => $":{x.ColumnName}"))});";
        }

        private string GetColumnNamesString(string[] columnNames)
        {
            return string.Join(",", columnNames);
        }
    }
}