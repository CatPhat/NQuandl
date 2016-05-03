using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Api.Metadata
{
    public interface IEntityMetadata<TEntity> where TEntity : DbEntity
    {
        Dictionary<string, DbEntityPropertyMetadata> GetProperyNameDbMetadata();
        TEntity GetEntityValueByPropertyName(TEntity entityWithData, string propertyName);
        string GetColumnNameBy(Expression<Func<TEntity, object>> expression);
        string GetTableName();
    }
}