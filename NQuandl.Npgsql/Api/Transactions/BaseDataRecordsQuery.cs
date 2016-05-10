namespace NQuandl.Npgsql.Api.Transactions
{
    public abstract class BaseDataRecordsQuery
    {
        protected BaseDataRecordsQuery(string tableName, string[] columnNames)
        {
            TableName = tableName;
            ColumnNames = columnNames;
        }

        protected BaseDataRecordsQuery(string tableName, string whereColumn, string[] columnNames, string query)
        {
            TableName = tableName;
            WhereColumn = whereColumn;
            QueryByString = query;
            ColumnNames = columnNames;
        }

        protected BaseDataRecordsQuery(string tableName, string whereColumn, string[] columnNames, int query)
        {
            TableName = tableName;
            WhereColumn = whereColumn;
            QueryByInt = query;
            ColumnNames = columnNames;
        }

        public string TableName { get; protected set; }
        public string WhereColumn { get; protected set; }
        public string OrderByColumn { get; protected set; }
        public string QueryByString { get; protected set; }
        public int? QueryByInt { get; protected set; }
        public string[] ColumnNames { get; protected set; }

        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }
}