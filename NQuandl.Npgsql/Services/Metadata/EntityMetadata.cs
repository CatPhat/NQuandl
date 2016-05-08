//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using NQuandl.Npgsql.Api.Entities;
//using NQuandl.Npgsql.Api.Metadata;
//using NQuandl.Npgsql.Services.Helpers;

//namespace NQuandl.Npgsql.Services.Metadata
//{
//    public class EntityMetadata<TEntity> : IEntityMetadata<TEntity> where TEntity : DbEntity
//    {
//        private readonly Type _type;

//        public EntityMetadata()
//        {
//            _type = typeof(TEntity);
//            TableName = GetTableNameFromAttributes();
//            DbEntityPropertyMetadatas = GetMetadataDictionary();
//        }

//        public Dictionary<string, DbEntityPropertyMetadata> DbEntityPropertyMetadatas { get; }

//        public string TableName { get; }


//        private Dictionary<string, PropertyInfo> GetPropertyInfos()
//        {
//            var properties = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
//            return properties.ToDictionary(propertyInfo => propertyInfo.Name);
//        }

//        private string GetTableNameFromAttributes()
//        {
//            var tableName = _type.GetCustomAttribute<DbTableNameAttribute>(false).TableName;
//            if (tableName == null)
//                throw new NullReferenceException("Missing DbTableName attribute");

//            return tableName;
//        }

//        private Dictionary<string, DbEntityPropertyMetadata> GetMetadataDictionary()
//        {
//            var typeProperties = _type.GetProperties();
//            var dbColumnAttributes = typeProperties.ToDictionary(x => x.Name,
//                x => x.GetCustomAttribute<DbColumnInfoAttribute>(false));
//            if (dbColumnAttributes.Count != typeProperties.Length)
//                throw new Exception("Missing Property Attribute(s)");

//            var dbColumnDictionary = dbColumnAttributes.ToDictionary(
//                dbColumnInfoAttribute => dbColumnInfoAttribute.Key,
//                dbColumnInfoAttribute => new DbEntityPropertyMetadata
//                {
//                    ColumnIndex = dbColumnInfoAttribute.Value.ColumnIndex,
//                    DbType = dbColumnInfoAttribute.Value.DbType,
//                    ColumnName = dbColumnInfoAttribute.Value.ColumnName,
//                    IsNullable = dbColumnInfoAttribute.Value.IsNullable,
//                    IsStoreGenerated = dbColumnInfoAttribute.Value.IsStoreGenerated
//                });

//            var propertyInfos = GetPropertyInfos();
//            foreach (var dbEntityPropertyMetadata in dbColumnDictionary)
//            {
//                dbEntityPropertyMetadata.Value.PropertyInfo = propertyInfos[dbEntityPropertyMetadata.Key];
//            }

//            return dbColumnDictionary;
//        }



//    }
//}