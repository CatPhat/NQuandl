using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using NpgsqlTypes;
using NQuandl.Npgsql.Api.Entities;

namespace NQuandl.Npgsql.Api.Metadata
{
    public interface IEntityMetadataCacheInitializer<TEntity> where TEntity : DbEntity
    {
        Dictionary<string, string> CreateColumnNames();
        Dictionary<string, int> CreateDbColumnIndex();
        Dictionary<string, NpgsqlDbType> CreateDbTypesDictionary();
        Dictionary<Expression, string> CreateExpressionToPropertyNameDictionary();
        Dictionary<string, bool> CreateIsDbNullableDictionary();
        Dictionary<string, bool> CreateIsDbStoreGeneratedDictionary();
        Dictionary<string, PropertyInfo> CreatePropertyInfosDictionary();
        string GetTableName();
    }
}