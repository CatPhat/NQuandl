using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Services.Metadata
{
    public class EntityMetadataProvider<TEntity> : IProvideEntityMetadata<TEntity> where TEntity : DbEntity
    {
        private readonly Dictionary<string, DbEntityPropertyMetadata> _propertyNameDbMetadata;
        private readonly Dictionary<Expression<Func<TEntity, object>>, DbEntityPropertyMetadata> _funcToPropertyNameDictionary;
        private readonly string _tableName;
        private readonly Type _type;

        public EntityMetadataProvider()
        {
            _type = typeof (TEntity);
            _tableName = GetTableName();
            _propertyNameDbMetadata = GetMetadataDictionary();
            _funcToPropertyNameDictionary = GetFuncToPropertyNameDictionary();
        }

        public Dictionary<string, DbEntityPropertyMetadata> GetProperyNameDbMetadata()
        {
            return _propertyNameDbMetadata;
        }

        public Dictionary<Expression<Func<TEntity, object>>, DbEntityPropertyMetadata> GetEntityPropertyMetadatas()
        {
            return _funcToPropertyNameDictionary;
        }


        string IProvideEntityMetadata<TEntity>.GetTableName()
        {
            return _tableName;
        }

        private Dictionary<string, PropertyInfo> GetPropertyInfos()
        {
            var properties = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return properties.ToDictionary(propertyInfo => propertyInfo.Name);
        }

        private string GetTableName()
        {
            var tableName = _type.GetCustomAttribute<DbTableNameAttribute>(false).TableName;
            if (tableName == null)
                throw new NullReferenceException("Missing DbTableName attribute");

            return tableName;
        }

        private Dictionary<Expression<Func<TEntity, object>>, DbEntityPropertyMetadata> GetFuncToPropertyNameDictionary()
        {
            var properties = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

           return properties.ToDictionary<PropertyInfo, Expression<Func<TEntity, object>>, DbEntityPropertyMetadata>(propertyInfo => (entity => propertyInfo), propertyInfo => _propertyNameDbMetadata[propertyInfo.Name]);
        }
    
        private Dictionary<string, DbEntityPropertyMetadata> GetMetadataDictionary()
        {
            var typeProperties = _type.GetProperties();
            var dbColumnAttributes = typeProperties.ToDictionary(x => x.Name,
                x => x.GetCustomAttribute<DbColumnInfoAttribute>(false));
            if (dbColumnAttributes.Count != typeProperties.Length)
                throw new Exception("Missing Property Attribute(s)");

            var dbColumnDictionary = dbColumnAttributes.ToDictionary(
                dbColumnInfoAttribute => dbColumnInfoAttribute.Key,
                dbColumnInfoAttribute => new DbEntityPropertyMetadata
                {
                    ColumnIndex = dbColumnInfoAttribute.Value.ColumnIndex,
                    DbType = dbColumnInfoAttribute.Value.DbType,
                    ColumnName = dbColumnInfoAttribute.Value.ColumnName
                });

            var propertyInfos = GetPropertyInfos();
            foreach (var dbEntityPropertyMetadata in dbColumnDictionary)
            {
                dbEntityPropertyMetadata.Value.PropertyInfo = propertyInfos[dbEntityPropertyMetadata.Key];
            }

            return dbColumnDictionary;
        }
    }



}