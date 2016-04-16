using NpgsqlTypes;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Entities
{
    [DbTableName("dataset_column_names")]
    public class DatasetColumnName
    {
        [DbColumnInfo(0, "id", NpgsqlDbType.Integer)]
        public int Id { get; set; }

        [DbColumnInfo(1, "column_index", NpgsqlDbType.Integer)]
        public int ColumnIndex { get; set; }

        [DbColumnInfo(2, "column_name", NpgsqlDbType.Text)]
        public string ColumnName { get; set; }

        [DbColumnInfo(3, "dataset_id", NpgsqlDbType.Integer)]
        public int DatasetId { get; set; }
    }
}