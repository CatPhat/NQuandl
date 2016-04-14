using System;

namespace NQuandl.Npgsql.Services.Helpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class DbColumnInfoAttribute : Attribute
    {
        public DbColumnInfoAttribute(int columnIndex, string columnName)
        {
            ColumnIndex = columnIndex;
            ColumnName = columnName;
        }

        public int ColumnIndex { get; }
        public string ColumnName { get; }
    }
}