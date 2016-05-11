using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using NpgsqlTypes;
using NQuandl.Npgsql.Api.Entities;

namespace NQuandl.Npgsql.Api.Metadata
{
    public interface IEntityMetadataCache<TEntity> where TEntity : DbEntity {
        string GetTableName();
        string GetPropertyName(Expression<Func<TEntity, object>> expression);
        string GetColumnName(Expression<Func<TEntity, object>> expression);
        string GetColumnName(string propertyName);
        int GetColumnIndex(string propertyName);
        bool GetIsNullable(string propertyName);
        bool GetIsStoreGenerated(string propertyName);
        NpgsqlDbType GetNpgsqlDbType(string propertyName);
        PropertyInfo GetPropertyInfo(string propertyName);
        List<PropertyInfo> GetPropertyInfos();
    }
}