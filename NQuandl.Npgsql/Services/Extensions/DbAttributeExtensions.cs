//using System.Linq;
//using NpgsqlTypes;
//using NQuandl.Npgsql.Services.Helpers;

//namespace NQuandl.Npgsql.Services.Extensions
//{
//    public static class DbAttributeExtensions
//    {
//        public static int GetColumnIndexByPropertName(this DbEntityMetadata metadata,
//            string propertName)
//        {
//            return metadata.GetColumnInfoAttributeByPropertyName(propertName).ColumnIndex;
//        }

//        public static DbEntityPropertyMetadata GetColumnInfoAttributeByPropertyName(
//            this DbEntityMetadata metadata, string propertyName)
//        {
//            return metadata.PropertyNameDbMetadata[propertyName];
//        }

//        public static string GetColumnNameByPropertyName(this DbEntityMetadata metadata,
//            string propertyName)
//        {
//            return metadata.PropertyNameDbMetadata[propertyName].ColumnName;
//        }

//        public static NpgsqlDbType GetNpgsqlDbTypeByPropertyName(this DbEntityMetadata metadata,
//            string propertyName)
//        {
//            return metadata.PropertyNameDbMetadata[propertyName].DbType;
//        }

      
//    }
//}