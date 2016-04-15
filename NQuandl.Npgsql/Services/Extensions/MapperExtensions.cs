using System;
using System.Linq.Expressions;
using NQuandl.Npgsql.Api;

namespace NQuandl.Npgsql.Services.Extensions
{
    public static class MapperExtensions
    {
        public static string GetTableName<TEntity>(this IMapDataRecordToEntity<TEntity> mapper)
        {
            return mapper.AttributeMetadata.TableName;
        }

        public static string GetColumnNames<TEntity>(this IMapDataRecordToEntity<TEntity> mapper)
        {
            return mapper.AttributeMetadata.GetColumnNames();
        }

        public static string GetColumnNameByPropertyName<TEntity>(this IMapDataRecordToEntity<TEntity> mapper,
            Expression<Func<TEntity, object>> entityProperty)
        {
            var name = ((MemberExpression) entityProperty.Body).Member.Name;
            return mapper.AttributeMetadata.GetColumnNameByPropertyName(name);
        }
    }
}