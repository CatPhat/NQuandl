using NpgsqlTypes;

namespace NQuandl.Npgsql.Api.DTO
{
    public class DbInsertData
    {
        public object Data { get; set; }
        public NpgsqlDbType DbType { get; set; }
        public string ColumnName { get; set; }
        public int ColumnIndex { get; set; }
        public bool IsNullable { get; set; }
        public bool IsStoreGenerated { get; set; }
    }


}