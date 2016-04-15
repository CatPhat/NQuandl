using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Entities
{
    [DbTableName("dataset_column_names")]
    public class DatasetColumnName
    {
        [DbColumnInfo(0, "id")]
        public int Id { get; set; }

        [DbColumnInfo(1, "column_index")]
        public int ColumnIndex { get; set; }

        [DbColumnInfo(2, "column_name")]
        public string ColumnName { get; set; }

        [DbColumnInfo(3, "dataset_id")]
        public int DatasetId { get; set; }
    }
}