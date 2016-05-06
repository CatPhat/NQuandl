using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Services.Helpers;
using NQuandl.Npgsql.Services.Transactions;

namespace NQuandl.Npgsql.Api.Metadata
{
    public interface IEntityMetadata<TEntity> where TEntity : DbEntity
    {
        Dictionary<string, DbEntityPropertyMetadata> GetProperyNameDbMetadata();
        object GetEntityValueByPropertyName(TEntity entityWithData, string propertyName);
        TEntity CreateEntity(IDataRecord record);
        string GetColumnNameBy(Expression<Func<TEntity, object>> expression);
        string GetTableName();
        List<DbData> GetDbDatas(TEntity entity);
    }
}