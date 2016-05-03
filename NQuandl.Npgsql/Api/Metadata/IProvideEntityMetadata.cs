using System.Collections.Generic;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Api.Metadata
{
    public interface IProvideEntityMetadata<TEntity> where TEntity : DbEntity
    {
        string GetTableName();
        Dictionary<string, DbEntityPropertyMetadata> GetProperyNameDbMetadata();
    }
}