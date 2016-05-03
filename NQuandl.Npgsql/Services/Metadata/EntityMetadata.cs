using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Services.Metadata
{

    public class EntityMetadata<TEntity> : IEntityMetadata<TEntity> where TEntity : DbEntity
    {
        private readonly IProvideEntityMetadata<TEntity> _metadata;

        public EntityMetadata(IProvideEntityMetadata<TEntity> metadata)
        {
            _metadata = metadata;
        }

        public string TableName => _metadata.GetTableName();

        public Dictionary<string, DbEntityPropertyMetadata> PropertyNameDbMetadataDictionary
            => _metadata.GetProperyNameDbMetadata();

        public TEntity GetEntityValueByPropertyName(TEntity entityWithData, string propertyName) 
        {
            return (TEntity) _metadata.GetProperyNameDbMetadata()[propertyName].PropertyInfo.GetValue(entityWithData,
              new  object[] { });
        }

        public string GetColumnNameBy(Expression<Func<TEntity, object>> expression)
        {
            var expressionDetail = ExpressionDetail.Create(expression);
           
            return _metadata.GetProperyNameDbMetadata()[expressionDetail.Name].ColumnName;
        }

      
    }
}