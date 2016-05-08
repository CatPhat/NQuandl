using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Api.Transactions;

namespace NQuandl.Npgsql.Services.Mappers
{
    public class EntitySqlMapper<TEntity> : IEntitySqlMapper<TEntity> where TEntity : DbEntity
    {
        private readonly ISqlMapper _sqlMapper;
        private readonly IEntityMetadata<TEntity> _metadata;

        public EntitySqlMapper([NotNull] ISqlMapper sqlMapper, [NotNull] IEntityMetadata<TEntity> metadata)
        {
            if (sqlMapper == null)
                throw new ArgumentNullException(nameof(sqlMapper));
            if (metadata == null)
                throw new ArgumentNullException(nameof(metadata));
            _sqlMapper = sqlMapper;
            _metadata = metadata;
        }

        public string GetSelectSqlBy(ReaderQuery query)
        {
            throw new NotImplementedException();
        }

        public string GetBulkInsertSql()
        {
            throw new NotImplementedException();
        }

        public string GetInsertSql(List<DbData> dbDatas, string tableName)
        {
            throw new NotImplementedException();
        }
    }

    public class SqlMapper : ISqlMapper
    {
        public string GetSelectSqlBy(ReaderQuery query)
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

        public string GetBulkInsertSql(BulkInsertCommand command)
        {
            var columnNames =
                GetColumnNamesString(
                    command.ColumnNameWithIndices.OrderBy(x => x.ColumnIndex).Select(y => y.ColumnName).ToArray());
            return
                $"COPY {command.TableName} " +
                $"({columnNames}) FROM STDIN (FORMAT BINARY)";
        }

        public string GetInsertSql(List<DbData> dbDatas, string tableName)
        {
            return
                $"INSERT INTO {tableName} ({string.Join(",", dbDatas.Select(x => x.ColumnName))}) " +
                $"VALUES ({string.Join(",", dbDatas.Select(x => $":{x.ColumnName}"))});";
        }

        private string GetColumnNamesString(string[] columnNames)
        {
            return string.Join(",", columnNames);
        }
    }

   
}