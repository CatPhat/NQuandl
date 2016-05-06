using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Domain.Queries;

namespace NQuandl.Npgsql.Services.Mappers
{
    public class EntitySqlMapper<TEntity> : IEntitySqlMapper<TEntity> where TEntity : DbEntity
    {
        private readonly string _bulkInsertSql;

        private readonly string _columnNames;
        private readonly string _columnNamesWithoutId;
        private readonly IEntityMetadata<TEntity> _entityMetadata;


        public EntitySqlMapper([NotNull] IEntityMetadata<TEntity> entityMetadata)
        {
            if (entityMetadata == null)
                throw new ArgumentNullException(nameof(entityMetadata));

            _entityMetadata = entityMetadata;


            _columnNames = GetColumnNames();
            _columnNamesWithoutId = GetColumnNamesWithoutId();
            _bulkInsertSql = GetBulkInsertSql();
        }

        public string BulkInsertSql()
        {
            return _bulkInsertSql;
        }

        public string GetSelectSqlBy(EntitiesReaderQuery<TEntity> query)
        {
            var queryString = new StringBuilder($"SELECT {_columnNames} FROM {_entityMetadata.GetTableName()}");

            if (query.Column != null)
            {
                if (!query.QueryByInt.HasValue && string.IsNullOrEmpty(query.QueryByString))
                {
                    throw new Exception("missing value for where clause.");
                }
                var whereValue = query.QueryByInt.HasValue ? $"{query.QueryByInt.Value}" : $"'{query.QueryByString}'";
                queryString.Append($" WHERE {_entityMetadata.GetColumnNameBy(query.Column)} = {whereValue}");
            }

            if (query.OrderBy != null)
            {
                queryString.Append($" ORDER BY {_entityMetadata.GetColumnNameBy(query.OrderBy)}");
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

        private string GetBulkInsertSql()
        {
            var columnNames = typeof(TEntity).BaseType == typeof(DbEntityWithSerialId)
                ? _columnNamesWithoutId
                : _columnNames;

            return
                $"COPY {_entityMetadata.GetTableName()} ({columnNames}) FROM STDIN (FORMAT BINARY)";
        }

      

        public string GetInsertSql(NpgsqlParameter[] parameters)
        {
            return
                $"INSERT INTO {_entityMetadata.GetTableName()} ({string.Join(",", parameters.Select(x => x.ParameterName))}) " +
                $"VALUES ({string.Join(",", parameters.Select(x => $":{x.ParameterName}"))});";
        }

        private string GetColumnNames()
        {
            return string.Join(",",
                _entityMetadata.GetProperyNameDbMetadata()
                    .OrderBy(y => y.Value.ColumnIndex)
                    .Select(x => x.Value.ColumnName));
        }

        private string GetColumnNamesWithoutId()
        {
            var properyNameDictionary = _entityMetadata.GetProperyNameDbMetadata();
            properyNameDictionary.Remove("Id");
            return string.Join(",",
                properyNameDictionary.OrderBy(y => y.Value.ColumnIndex).Select(x => x.Value.ColumnName));
        }
    }
}