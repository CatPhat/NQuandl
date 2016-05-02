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
 


        public EntitySqlMapper(IEntityMetadata<TEntity> entityMetadata)
        {
            _entityMetadata = entityMetadata;
            _bulkInsertSql = GetBulkInsertSql();
           
        }

        public string BulkInsertSql()
        {
            return _bulkInsertSql;
        }

        private string GetBulkInsertSql()
        {
            var columnNames = typeof (TEntity).GetGenericTypeDefinition() == typeof (DbEntityWithSerialId)
                ? GetColumnNamesWithoutId()
                : GetColumnNames();

            return
                $"COPY {_entityMetadata.TableName} ({columnNames}) FROM STDIN (FORMAT BINARY)";
        }

        public string GetSelectSqlBy(EntitiesReaderQuery<TEntity> query)
        {
            var queryString = new StringBuilder($"SELECT {_entityMetadata.TableName} FROM {_entityMetadata.TableName} ");

            if (query.Column != null)
            {
                queryString.Append($" WHERE {_entityMetadata.FuncPropertyMetadatas[query.Column].ColumnName} = ");
                if (!query.QueryByInt.HasValue && string.IsNullOrEmpty(query.QueryByString))
                {
                    throw new Exception("missing value for where clause.");
                }
                queryString.Append(query.QueryByInt.HasValue ? $"{query.QueryByInt.Value} " : $"{query.QueryByString} ");
            }

            if (query.OrderBy != null)
            {
                queryString.Append($" ORDER BY {_entityMetadata.FuncPropertyMetadatas[query.OrderBy].ColumnName}");
            }

            if (query.Limit.HasValue)
            {
                queryString.Append($" LIMIT {query.Limit.Value} ");
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