using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Api.Metadata
{
    public interface IEntityMetadata<TEntity> where TEntity : DbEntity
    {
        Dictionary<string, DbEntityPropertyMetadata> GetProperyNameDbMetadata();
        object GetEntityValueByPropertyName(TEntity entityWithData, string propertyName);
        TEntity CreatEntity(IDataRecord record);
        string GetColumnNameBy(Expression<Func<TEntity, object>> expression);
        string GetTableName();
    }
}