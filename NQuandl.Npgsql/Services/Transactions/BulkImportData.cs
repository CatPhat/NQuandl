using NpgsqlTypes;

namespace NQuandl.Npgsql.Services.Transactions
{
    public class BulkImportData
    {
        public object Data { get; set; }
        public NpgsqlDbType DbType { get; set; }
    }
}