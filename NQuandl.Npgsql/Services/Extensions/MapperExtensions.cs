using System;
using System.Linq.Expressions;
using Npgsql;
using NQuandl.Npgsql.Api;
using NQuandl.Npgsql.Services.Helpers;

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

        public static string GetColumnNamesWithoutId<TEntity>(this IMapDataRecordToEntity<TEntity> mapper)
        {
            return mapper.AttributeMetadata.GetColumnNamesWithoutId();
        }

        public static string GetColumnNameByPropertyName<TEntity>(this IMapDataRecordToEntity<TEntity> mapper,
            Expression<Func<TEntity, object>> entityExpression)
        {
            var name = ReflectionExtensions<TEntity>.GetPropertyNameFromPropertyExpression(entityExpression);
            return mapper.AttributeMetadata.GetColumnNameByPropertyName(name);
        }

        public static NpgsqlParameter GetNpgsqlParameterByProperty<TEntity>(
            this IMapDataRecordToEntity<TEntity> mapper, Expression<Func<TEntity, object>> entityExpression,
            object parameterValue)
        {
            var name = ReflectionExtensions<TEntity>.GetPropertyNameFromPropertyExpression(entityExpression);
            var columnName = mapper.AttributeMetadata.GetColumnNameByPropertyName(name);
            var dbType = mapper.AttributeMetadata.GetNpgsqlDbTypeByPropertyName(name);
            return new NpgsqlParameter(columnName, dbType) {Value = parameterValue};
        }

        public static DbColumnInfoAttribute GetDbColumnInfoAttributeByProperty<TEntity>(
            this IMapDataRecordToEntity<TEntity> mapper, Expression<Func<TEntity, object>> entityExpression)
        {
            var name = ReflectionExtensions<TEntity>.GetPropertyNameFromPropertyExpression(entityExpression);
            return mapper.AttributeMetadata.GetColumnInfoAttributeByPropertyName(name);
        }
    }

    public static class ReflectionExtensions<TEntity>
    {
        public static string GetPropertyNameFromPropertyExpression(Expression<Func<TEntity, object>> entityExpression)
        {
            string name;
            var body = entityExpression.Body as MemberExpression;
            if (body != null)
            {
                name = body.Member.Name;
            }
            else
            {
                var operand = ((UnaryExpression)entityExpression.Body).Operand;
                name = ((MemberExpression)operand).Member.Name;
            }
            return name;
        }
    }
}