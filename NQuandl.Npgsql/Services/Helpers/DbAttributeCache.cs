using System;
using System.Linq;
using System.Reflection;

namespace NQuandl.Npgsql.Services.Helpers
{
    public static class DbAttributeCache<T>
    {
        public static readonly DbEntityAttributeMetadata AttributeMetadata;

        static DbAttributeCache()
        {
            AttributeMetadata = GetMetadata(typeof (T));
        }

        private static DbEntityAttributeMetadata GetMetadata(Type type)
        {
            var tableName = type.GetCustomAttribute<DbTableNameAttribute>(false).TableName;
            if (tableName == null)
                throw new NullReferenceException("Missing DbTableName attribute");

            var typeProperties = type.GetProperties();
            var dbColumnAttributes = typeProperties.ToDictionary(x => x.Name,
                x => x.GetCustomAttribute<DbColumnInfoAttribute>(false));
            if (!dbColumnAttributes.Any())
                throw new Exception("Missing Property Attributes");

            return new DbEntityAttributeMetadata
            {
                TableName = tableName,
                PropertyNameAttributeDictionary = dbColumnAttributes
            };
        }
    }
}