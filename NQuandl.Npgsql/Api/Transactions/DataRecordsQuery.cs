namespace NQuandl.Npgsql.Api.Transactions
{
    public class DataRecordsQuery
    {
        public string TableName { get; set; }
        public string WhereColumn { get; set; }
        public string OrderByColumn { get; set; }
        public string QueryByString { get; set; }
        public int? QueryByInt { get; set; }
        public string[] ColumnNames { get; set; }

        public int? Limit { get; set; }
        public int? Offset { get; set; }
    }
}