using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using JetBrains.Annotations;
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
            var dbDatas = GetDbDatas(entity, true);
            var insertStatement = GetInsertSql(dbDatas);
            return new InsertData
            {
                DbDatas = dbDatas,
                SqlStatement = insertStatement
            };
        }

        public List<DbData> GetDbDatas(TEntity entity, bool excludeIsStoreGenerated = false)
        {
            var orderedEnumerable =
                _entityMetadata.GetProperyNameDbMetadata()
                    .OrderBy(y => y.Value.ColumnIndex);
            var dbDatas = (from keyValue in orderedEnumerable
                let data = _entityMetadata.GetEntityValueByPropertyName(entity, keyValue.Key)
                select new DbData
                {
                    Data = data,
                    DbType = keyValue.Value.DbType,
                    ColumnName = keyValue.Value.ColumnName,
                    ColumnIndex = keyValue.Value.ColumnIndex,
                    IsNullable = keyValue.Value.IsNullable,
                    IsStoreGenerated = keyValue.Value.IsStoreGenerated
                });
            if (excludeIsStoreGenerated)
            {
                dbDatas = dbDatas.Where(x => x.IsStoreGenerated == false);
            }

            return dbDatas.ToList();
        }

        public BulkInsertData GetBulkInsertData(IObservable<TEntity> entities)
        {
            return new BulkInsertData
            {
                SqlStatement = GetBulkInsertSql(),
                DbDatasObservable = GetBulkImportDatas(entities)
            };
        }

        private IObservable<List<DbData>> GetBulkImportDatas(IObservable<TEntity> entities)
        {
            return Observable.Create<List<DbData>>(observer =>
                entities.Subscribe(entity => observer.OnNext(GetDbDatas(entity).OrderBy(x => x.ColumnIndex).ToList()),
                onCompleted: observer.OnCompleted,
                onError: ex => { throw new Exception(ex.Message); }));
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


        private string GetInsertSql(List<DbData> dbDatas)
        {
            return
                $"INSERT INTO {_entityMetadata.GetTableName()} ({string.Join(",", dbDatas.Select(x => x.ColumnName))}) " +
                $"VALUES ({string.Join(",", dbDatas.Select(x => $":{x.ColumnName}"))});";
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