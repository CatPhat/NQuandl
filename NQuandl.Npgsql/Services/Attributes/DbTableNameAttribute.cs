using System;

namespace NQuandl.Npgsql.Services.Attributes
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