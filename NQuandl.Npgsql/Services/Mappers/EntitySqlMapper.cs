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
using NQuandl.Npgsql.Services.Transactions;

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

        public InsertData GetInsertData(TEntity entity)
        {
            var parameters = GetParameters(entity);
            var insertStatement = GetInsertSql(parameters);

            return new InsertData
            {
                Parameters = parameters,
                SqlStatement = insertStatement
            };
        }

        public IEnumerable<DbData> GetDbDatas(TEntity entity)
        {
            var orderedEnumerable = _entityMetadata.GetProperyNameDbMetadata();
            return (from keyValue in orderedEnumerable
                    let data = _entityMetadata.GetEntityValueByPropertyName(entity, keyValue.Key)
                select new DbData
                {
                    Data = data,
                    DbType = keyValue.Value.DbType,
                    ColumnName = keyValue.Value.ColumnName,
                    ColumnIndex = keyValue.Value.ColumnIndex,
                    IsNullable = keyValue.Value.IsNullable
                });
        }


        //todo am i mixing concerns with depending on NpgsqlParameters?
        private NpgsqlParameter[] GetParameters(TEntity entity)
        {
            var datas = GetDbDatas(entity);
            return datas.Select(dbData => new NpgsqlParameter(dbData.ColumnName, dbData.DbType)
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

        private string GetColumnNamesWithoutId()
        {
            var properyNameDictionary = _entityMetadata.GetProperyNameDbMetadata();
            return string.Join(",",
                properyNameDictionary.Where(x => x.Key != "Id").OrderBy(y => y.Value.ColumnIndex).Select(x => x.Value.ColumnName));
        }
    }
}