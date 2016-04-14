using System;

namespace NQuandl.Npgsql.Services.Helpers
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DbTableNameAttribute : Attribute
    {
        public DbTableNameAttribute(string tableName)
        {
            TableName = tableName;
        }

        public string TableName { get; }
    }
}