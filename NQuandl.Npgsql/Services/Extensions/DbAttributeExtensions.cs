using System.Linq;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Services.Extensions
{
    public static class DbAttributeExtensions
    {
        public static int GetColumnIndexByPropertName(this DbEntityAttributeMetadata attributeMetadata,
            string propertName)
        {
            return attributeMetadata.GetColumnInfoAttributeByPropertyName(propertName).ColumnIndex;
        }

        public static DbColumnInfoAttribute GetColumnInfoAttributeByPropertyName(
            this DbEntityAttributeMetadata attributeMetadata, string propertyName)
        {
            return attributeMetadata.PropertyNameAttributeDictionary[propertyName];
        }

        public static string GetColumnNameByPropertyName(this DbEntityAttributeMetadata attributeMetadata,
            string propertyName)
        {
            return attributeMetadata.PropertyNameAttributeDictionary[propertyName].ColumnName;
        }

        public static string GetColumnNames(this DbEntityAttributeMetadata attributeMetadata)
        {
            return string.Join(",", attributeMetadata.PropertyNameAttributeDictionary.Select(x => x.Value.ColumnName));
        }
    }
}