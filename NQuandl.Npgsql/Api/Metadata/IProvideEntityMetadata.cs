using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Api.Metadata
{
    public interface IProvideEntityMetadata<TEntity> where TEntity : DbEntity
    {
        string GetTableName();
        Dictionary<string, DbEntityPropertyMetadata> GetProperyNameDbMetadata();
        Dictionary<Expression<Func<TEntity, object>>, DbEntityPropertyMetadata> GetEntityPropertyMetadatas();
    }
}