using NpgsqlTypes;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Services.Transactions
{
    public class DbData
    {
        public object Data { get; set; }
        public NpgsqlDbType DbType { get; set; }
        public string ColumnName { get; set; }
        public int ColumnIndex { get; set; }
        public bool IsNullable { get; set; }
    }


}