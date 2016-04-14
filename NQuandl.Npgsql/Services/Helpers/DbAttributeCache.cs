using System;
using System.Linq;
using System.Reflection;

namespace NQuandl.Npgsql.Services.Helpers
{
    public static class DbAttributeCache<T>
    {
        static DbAttributeCache()
        {
            AttributeAttributeMetadata = GetMetadata(typeof (T));
        }

        public static readonly DbEntityAttributeMetadata AttributeAttributeMetadata;

        private static DbEntityAttributeMetadata GetMetadata(Type type)
        {
            var tableName = type.GetCustomAttribute<DbTableNameAttribute>(false).TableName;
            if (tableName == null)
                throw new NullReferenceException("Missing DbTableName attribute");

            var typeProperties = type.GetProperties();
            var dbColumnAttributes = typeProperties.ToDictionary(x => x.Name, x => x.GetCustomAttribute< DbColumnInfoAttribute>(false));
            if (!dbColumnAttributes.Any())
                throw new Exception("Missing Property Attributes");

            return new DbEntityAttributeMetadata
            {
                TableName = tableName,
                PropertyNameAttributeDictionary = dbColumnAttributes

            };
        }

     
    }

    public static class DbAttributeExtensions
    {
        public static int GetColumnIndexByPropertName(this DbEntityAttributeMetadata attributeMetadata, string propertName)
        {
            return attributeMetadata.GetColumnInfoAttributeByPropertyName(propertName).ColumnIndex;
        }

        public static DbColumnInfoAttribute GetColumnInfoAttributeByPropertyName(this DbEntityAttributeMetadata attributeMetadata, string propertyName)
        {
            return attributeMetadata.PropertyNameAttributeDictionary[propertyName];
        }
    }
}