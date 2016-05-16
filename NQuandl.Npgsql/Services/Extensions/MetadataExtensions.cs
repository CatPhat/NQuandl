using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using NQuandl.Npgsql.Api.DTO;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Api.Transactions;

namespace NQuandl.Npgsql.Services.Extensions
{
    public static class MetadataExtensions
    {
        public static IEnumerable<ColumnNameWithIndex> ToColumnNameWithIndices<TEntity>(
            this IEntityMetadataCache<TEntity> metadata) where TEntity : DbEntity
        {
            return metadata.GetPropertyInfos()
                .Select(propertyInfo => new ColumnNameWithIndex
                {
                    ColumnName = metadata.GetColumnNameOrDefault(propertyInfo.Name),
                    ColumnIndex = metadata.GetColumnIndex(propertyInfo.Name)
                }).OrderBy(x => x.ColumnIndex);
        }

        public static List<DbInsertData> CreateInsertDatas<TEntity>(this IEntityMetadataCache<TEntity> metadata,
            [NotNull] TEntity entity) where TEntity : DbEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            return (from propertyInfo in metadata.GetPropertyInfos()
                let data = metadata.GetPropertyInfo(propertyInfo.Name).GetValue(entity, new object[] {})
                let propertyName = propertyInfo.Name
                select new DbInsertData
                {
                    Data = data,
                    DbType = metadata.GetNpgsqlDbType(propertyName),
                    ColumnName = metadata.GetColumnNameOrDefault(propertyName),
                    ColumnIndex = metadata.GetColumnIndex(propertyName),
                    IsNullable = metadata.GetIsNullable(propertyName),
                    IsStoreGenerated = metadata.GetIsStoreGenerated(propertyName)
                }).ToList();
        }

        public static DataRecordsQuery CreateDataRecordsQuery<TEntity,TQuery>(this IEntityMetadataCache<TEntity> metadata, TQuery query) where TQuery : BaseEntitiesQuery<TEntity> where TEntity : DbEntity
        {
            var whereColumnPropertyName = metadata.GetPropertyName(query.WhereColumn);
            var orderByPropertyName = metadata.GetPropertyName(query.OrderByColumn);
            var properties = metadata.GetPropertyInfos();
            var columnNames = properties.Select(propertyInfo => metadata.GetColumnNameOrDefault(propertyInfo.Name)).ToArray();
            return new DataRecordsQuery
            {
                TableName = metadata.GetTableName(),
                WhereColumn = metadata.GetColumnNameOrDefault(whereColumnPropertyName),
                OrderByColumn = metadata.GetColumnNameOrDefault(orderByPropertyName),
                Limit = query.Limit,
                Offset = query.Offset,
                QueryByInt = query.QueryByInt,
                QueryByString = query.QueryByString,
                ColumnNames = columnNames
            };
        }
    }
}