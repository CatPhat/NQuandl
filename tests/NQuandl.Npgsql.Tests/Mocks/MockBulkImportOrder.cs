using NpgsqlTypes;

namespace NQuandl.Npgsql.Tests.Mocks
{
    public class MockBulkImportOrder
    {
        public int RowIndex { get; set; }
        public int ColumnIndex { get; set; }
        public object Data { get; set; }
        public NpgsqlDbType DbType { get; set; }
    }
}