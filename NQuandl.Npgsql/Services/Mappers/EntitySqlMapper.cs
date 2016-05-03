using System;
using System.Linq;
using System.Text;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Services.Transactions;

namespace NQuandl.Npgsql.Services.Mappers
{
    public interface IEntitySqlMapper<TEntity> where TEntity : DbEntity
    {
        string BulkInsertSql();
    }

    public class EntitySqlMapper<TEntity> : IEntitySqlMapper<TEntity> where TEntity : DbEntity
    {
        private readonly string _bulkInsertSql;
        private readonly IEntityMetadata<TEntity> _entityMetadata;
        private readonly string _columnNames;
        private readonly string _columnNamesWithoutId;


        public EntitySqlMapper(IEntityMetadata<TEntity> entityMetadata)
        {
            _entityMetadata = entityMetadata;
            _bulkInsertSql = GetBulkInsertSql();

            _columnNames = GetColumnNames();
            _columnNamesWithoutId = GetColumnNamesWithoutId();

        }

        public string BulkInsertSql()
        {
            return _bulkInsertSql;
        }

        private string GetBulkInsertSql()
        {
            var columnNames = typeof(TEntity).BaseType == typeof(DbEntityWithSerialId)
                ? _columnNamesWithoutId
                : _columnNames;

            return
                $"COPY {_entityMetadata.TableName} ({columnNames}) FROM STDIN (FORMAT BINARY)";
        }

        public string GetSelectSqlBy(EntitiesReaderQuery<TEntity> query)
        {
            var queryString = new StringBuilder($"SELECT {_columnNames} FROM {_entityMetadata.TableName}");
            
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

        private string GetColumnNames()
        {
            return string.Join(",",
                _entityMetadata.PropertyNameDbMetadataDictionary
                    .OrderBy(y => y.Value.ColumnIndex)
                    .Select(x => x.Value.ColumnName));
        }

        private string GetColumnNamesWithoutId()
        {
            var properyNameDictionary = _entityMetadata.PropertyNameDbMetadataDictionary;
            properyNameDictionary.Remove("Id");
            return string.Join(",",
                properyNameDictionary.OrderBy(y => y.Value.ColumnIndex).Select(x => x.Value.ColumnName));
        }
    }
}