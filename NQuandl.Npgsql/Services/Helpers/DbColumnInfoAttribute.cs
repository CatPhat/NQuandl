using System;
using NpgsqlTypes;

namespace NQuandl.Npgsql.Services.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbColumnInfoAttribute : Attribute
    {
        public DbColumnInfoAttribute(int columnIndex, string columnName, NpgsqlDbType dbType)
        {
            ColumnIndex = columnIndex;
            ColumnName = columnName;
            DbType = dbType;
        }

        public int ColumnIndex { get; }
        public string ColumnName { get; }
        public NpgsqlDbType DbType { get; }
    }
}