using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Api.Metadata;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Services.Metadata
{
    public class EntityMetadata<TEntity> : IEntityMetadata<TEntity> where TEntity : DbEntity
    {
        private readonly Dictionary<string, DbEntityPropertyMetadata> _propertyNameDbMetadata;
        private readonly string _tableName;
        private readonly Type _type;

        public EntityMetadata()
        {
            _type = typeof (TEntity);
            _tableName = GetTableNameFromAttributes();
            _propertyNameDbMetadata = GetMetadataDictionary();
        
        }

        public Dictionary<string, DbEntityPropertyMetadata> GetProperyNameDbMetadata()
        {
            return _propertyNameDbMetadata;
        }

        public object GetEntityValueByPropertyName(TEntity entityWithData, string propertyName)
        {
            return _propertyNameDbMetadata[propertyName].PropertyInfo.GetValue(entityWithData,
              new object[] { });
        }

        public TEntity CreatEntity(IDataRecord record)
        {
            var entity = (TEntity)Activator.CreateInstance(typeof(TEntity), new object[] { });
            var metadata = _propertyNameDbMetadata;
            foreach (var dbEntityPropertyMetadata in metadata)
            {
                var entityProperty = dbEntityPropertyMetadata.Value.PropertyInfo;
                var recordValue = record[dbEntityPropertyMetadata.Value.ColumnIndex];
                if (recordValue != DBNull.Value)
                {
                    entityProperty.SetValue(entity, recordValue);
                }
            }

            return entity;
        }

        public string GetColumnNameBy(Expression<Func<TEntity, object>> expression)
        {
            var expressionDetail = ExpressionDetail.Create(expression);

            return _propertyNameDbMetadata[expressionDetail.Name].ColumnName;
        }

        public string GetTableName()
        {
            return _tableName;
        }

        private Dictionary<string, PropertyInfo> GetPropertyInfos()
        {
            var properties = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return properties.ToDictionary(propertyInfo => propertyInfo.Name);
        }

        private string GetTableNameFromAttributes()
        {
            var tableName = _type.GetCustomAttribute<DbTableNameAttribute>(false).TableName;
            if (tableName == null)
                throw new NullReferenceException("Missing DbTableName attribute");

            return tableName;
        }

        //private Dictionary<Expression, DbEntityPropertyMetadata> GetFuncToPropertyNameDictionary()
        //{
        //    var properties = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        //    var dictionary = new Dictionary<Expression, DbEntityPropertyMetadata>();

        

        //    foreach (var propertyInfo in properties)
        //    {
                
            
        //        var parameter = Expression.Parameter(_type, _type.Name);
        //        var property = Expression.Property(parameter, propertyInfo);

        //        var funcType = typeof(Func<,>).MakeGenericType(_type, propertyInfo.PropertyType);
        //        var lambda =  Expression.Lambda(funcType, property, parameter);
        //        //var expression = Expression.Lambda<Func<TEntity, object>>();

        //        dictionary.Add(lambda, _propertyNameDbMetadata[propertyInfo.Name]);
        //    }
        //    return dictionary;
        //}

   

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