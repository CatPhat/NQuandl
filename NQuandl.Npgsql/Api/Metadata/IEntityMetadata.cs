using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Api.Metadata
{
    public interface IEntityMetadata<TEntity> where TEntity : DbEntity
    {
        string TableName { get; }
        Dictionary<string, DbEntityPropertyMetadata> PropertyNameDbMetadataDictionary { get; }
        TEntity GetEntityValueByPropertyName(TEntity entityWithData, string propertyName);
        Dictionary<Expression<Func<TEntity, object>>, DbEntityPropertyMetadata> FuncPropertyMetadatas { get; }
      
    }
}