using System;
using NpgsqlTypes;

namespace NQuandl.Npgsql.Services.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbColumnInfoAttribute : Attribute
    {
        public DbColumnInfoAttribute(int columnIndex, string columnName, NpgsqlDbType dbType, bool isNullable = false)
        {
            ColumnIndex = columnIndex;
            ColumnName = columnName;
            DbType = dbType;
            IsNullable = isNullable;
        }

        public int ColumnIndex { get; }
        public string ColumnName { get; }
        public NpgsqlDbType DbType { get; }
        public bool IsNullable { get; set; }
    }
}