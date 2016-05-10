using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NpgsqlTypes;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Services.Attributes;

namespace NQuandl.Npgsql.Services.Metadata
{
    public class EntityMetadataCacheInitializer<TEntity> : IEntityMetadataCacheInitializer<TEntity> where TEntity : DbEntity 
    {
        private readonly PropertyInfo[] _entityPropertyInfos;
        private readonly Type _entityType;
        private readonly Dictionary<string, DbColumnInfoAttribute> _propertyNamesDbColumnInfoAttributes;

        public EntityMetadataCacheInitializer()
        {
            _entityType = typeof(TEntity);
            _entityPropertyInfos = _entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            _propertyNamesDbColumnInfoAttributes = CreateDbColumnInfoAttributeDictionary();
        }

        
        public Dictionary<string, PropertyInfo> CreatePropertyInfosDictionary()
        {
            return _entityPropertyInfos.ToDictionary(propertyInfo => propertyInfo.Name);
        }

        public Dictionary<string, NpgsqlDbType> CreateDbTypesDictionary()
        {
            return _entityPropertyInfos.ToDictionary(x => x.Name,
                x => _propertyNamesDbColumnInfoAttributes[x.Name].DbType);
        }

        public Dictionary<string, int> CreateDbColumnIndex()
        {
            return _entityPropertyInfos.ToDictionary(x => x.Name,
                x => _propertyNamesDbColumnInfoAttributes[x.Name].ColumnIndex);
        }

        public Dictionary<string, string> CreateColumnNames()
        {
            return _entityPropertyInfos.ToDictionary(x => x.Name,
                x => _propertyNamesDbColumnInfoAttributes[x.Name].ColumnName);
        }

        public Dictionary<string, bool> CreateIsDbNullableDictionary()
        {
            return _entityPropertyInfos.ToDictionary(x => x.Name,
                x => _propertyNamesDbColumnInfoAttributes[x.Name].IsNullable);
        }

        public Dictionary<string, bool> CreateIsDbStoreGeneratedDictionary()
        {
            return _entityPropertyInfos.ToDictionary(x => x.Name,
                x => _propertyNamesDbColumnInfoAttributes[x.Name].IsStoreGenerated);
        }

        public string GetTableName()
        {
            var tableName = _entityType.GetCustomAttribute<DbTableNameAttribute>(false).TableName;
            if (tableName == null)
                throw new NullReferenceException("Missing DbTableName attribute");

            return tableName;
        }

        private Dictionary<string, DbColumnInfoAttribute> CreateDbColumnInfoAttributeDictionary()
        {
            return _entityPropertyInfos.ToDictionary(x => x.Name,
                x => x.GetCustomAttribute<DbColumnInfoAttribute>(false));
        }

        public Dictionary<Expression, string> CreateExpressionToPropertyNameDictionary()
        {
            var dictionary = new Dictionary<Expression, string>();
            foreach (var propertyInfo in _entityPropertyInfos)
            {
                var parameter = Expression.Parameter(_entityType, _entityType.Name);
                var property = Expression.Property(parameter, propertyInfo);
                var funcType = typeof(Func<,>).MakeGenericType(_entityType, propertyInfo.PropertyType);
                var lambda = Expression.Lambda(funcType, property, parameter);
                //var expression = Expression.Lambda<Func<TEntity, object>>();
                dictionary.Add(lambda, propertyInfo.Name);
            }
            return dictionary;
        }
    }
}