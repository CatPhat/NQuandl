using System.Reflection;
using NpgsqlTypes;

namespace NQuandl.Npgsql.Services.Helpers
{
    public class DbEntityPropertyMetadata
    {
        public int ColumnIndex { get; set; }
        public string ColumnName { get; set; }
        public NpgsqlDbType DbType { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public bool IsNullable { get; set; }
        public bool IsStoreGenerated { get; set; }
    }
}