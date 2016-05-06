using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using Npgsql;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Api.Transactions;
using NQuandl.Npgsql.Services.Transactions;

namespace NQuandl.Npgsql.Services.Mappers
{
    public class EntitySqlMapper<TEntity> : IEntitySqlMapper<TEntity> where TEntity : DbEntity
    {
        private readonly string _bulkInsertSql;
        private readonly string _columnNames;
        private readonly IEntityMetadata<TEntity> _entityMetadata;


        public EntitySqlMapper([NotNull] IEntityMetadata<TEntity> entityMetadata)
        {
            if (entityMetadata == null)
                throw new ArgumentNullException(nameof(entityMetadata));

            _entityMetadata = entityMetadata;
            _columnNames = GetColumnNames();
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

        public InsertData GetInsertData(TEntity entity)
        {
            var dbData = GetDbDatas(entity).Where(x => x.IsStoreGenerated == false);
            var parameters = GetParameters(dbData);
            var insertStatement = GetInsertSql(parameters);
            return new InsertData
            {
                Parameters = parameters,
                SqlStatement = insertStatement
            };
        }

        public IEnumerable<DbData> GetDbDatas(TEntity entity)
        {
            var orderedEnumerable =
                _entityMetadata.GetProperyNameDbMetadata()
                .OrderBy(y => y.Value.ColumnIndex);
            return from keyValue in orderedEnumerable
                let data = _entityMetadata.GetEntityValueByPropertyName(entity, keyValue.Key)
                select new DbData
                {
                    Data = data,
                    DbType = keyValue.Value.DbType,
                    ColumnName = keyValue.Value.ColumnName,
                    ColumnIndex = keyValue.Value.ColumnIndex,
                    IsNullable = keyValue.Value.IsNullable,
                    IsStoreGenerated = keyValue.Value.IsStoreGenerated
                    
                };
        }


        private string GetColumnNamesIfNotStoreGenerated()
        {
            var properyNameDictionary = _entityMetadata.GetProperyNameDbMetadata();
            return string.Join(",",
                properyNameDictionary.Where(x => x.Value.IsStoreGenerated == false)
                    .OrderBy(y => y.Value.ColumnIndex)
                    .Select(x => x.Value.ColumnName));
        }

        private string GetBulkInsertSql()
        {
            return
                $"COPY {_entityMetadata.GetTableName()} ({GetColumnNamesIfNotStoreGenerated()}) FROM STDIN (FORMAT BINARY)";
        }


        //todo am i mixing concerns with depending on NpgsqlParameters?
        private NpgsqlParameter[] GetParameters(IEnumerable<DbData> dbDatas)
        {
            return dbDatas
            .Select(dbData => new NpgsqlParameter(dbData.ColumnName, dbData.DbType)
            {
                Value = dbData.Data,
                IsNullable = dbData.IsNullable
            }).ToArray();
        }


        private string GetInsertSql(NpgsqlParameter[] parameters)
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
    }
}