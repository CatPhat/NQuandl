using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NpgsqlTypes;
using NQuandl.Npgsql.Api.Entities;

namespace NQuandl.Npgsql.Services.Metadata
{
    public interface IEntityMetadataCache<TEntity> where TEntity : DbEntity {
        string GetTableName();
        string GetPropertyName(Expression<Func<TEntity, object>> expression);
        string GetColumnName(string propertyName);
        int GetColumnIndex(string propertyName);
        bool GetIsNullable(string propertyName);
        bool GetIsStoreGenerated(string propertyName);
        NpgsqlDbType GetNpgsqlDbType(string propertyName);
        PropertyInfo GetPropertyInfo(string propertyName);
        List<PropertyInfo> GetPropertyInfos();
    }

    public class EntityMetadataCache<TEntity> : IEntityMetadataCache<TEntity> where TEntity : DbEntity
    {
        private readonly string _dbTableName;
        private readonly Dictionary<Expression, string> _expressionPropertyNames;
        private readonly Dictionary<string, int> _propertyNameDbColumnIndex;
        private readonly Dictionary<string, string> _propertyNameDbColumnNames;
        private readonly Dictionary<string, bool> _propertyNameIsDbNullable;
        private readonly Dictionary<string, bool> _propertyNameIsDbStoreGenerated;
        private readonly Dictionary<string, NpgsqlDbType> _propertyNameNpgsqlDbTypes;
        private readonly Dictionary<string, PropertyInfo> _propertyNamePropertyInfos;

        public EntityMetadataCache(IEntityMetadataCacheInitializer<TEntity> initializer)
        {
            _dbTableName = initializer.GetTableName();
            _expressionPropertyNames = initializer.CreateExpressionToPropertyNameDictionary();
            _propertyNameDbColumnIndex = initializer.CreateDbColumnIndex();
            _propertyNameDbColumnNames = initializer.CreateColumnNames();
            _propertyNameIsDbNullable = initializer.CreateIsDbNullableDictionary();
            _propertyNameIsDbStoreGenerated = initializer.CreateIsDbStoreGeneratedDictionary();
            _propertyNameNpgsqlDbTypes = initializer.CreateDbTypesDictionary();
            _propertyNamePropertyInfos = initializer.CreatePropertyInfosDictionary();
        }

        public string GetTableName()
        {
            return _dbTableName;
        }

        public string GetPropertyName(Expression<Func<TEntity, object>> expression)
        {
            return _expressionPropertyNames[expression];
        }

        public string GetColumnName(string propertyName)
        {
            return _propertyNameDbColumnNames[propertyName];
        }

        public int GetColumnIndex(string propertyName)
        {
            return _propertyNameDbColumnIndex[propertyName];
        }

        public bool GetIsNullable(string propertyName)
        {
            return _propertyNameIsDbNullable[propertyName];
        }

        public bool GetIsStoreGenerated(string propertyName)
        {
            return _propertyNameIsDbStoreGenerated[propertyName];
        }

        public NpgsqlDbType GetNpgsqlDbType(string propertyName)
        {
            return _propertyNameNpgsqlDbTypes[propertyName];
        }

        public PropertyInfo GetPropertyInfo(string propertyName)
        {
            return _propertyNamePropertyInfos[propertyName];
        }

        public List<PropertyInfo> GetPropertyInfos()
        {
            return _propertyNamePropertyInfos.Select(x => x.Value).ToList();
        }

    }
}