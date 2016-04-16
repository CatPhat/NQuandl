using NpgsqlTypes;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Entities
{
    [DbTableName("database_datasets")]
    public class DatabaseDataset
    {
        [DbColumnInfo(0, "id", NpgsqlDbType.Integer)]
        public int Id { get; set; }

        [DbColumnInfo(1, "database_code", NpgsqlDbType.Text)]
        public string DatabaseCode { get; set; }

        [DbColumnInfo(2, "dataset_code", NpgsqlDbType.Text)]
        public string DatasetCode { get; set; }

        [DbColumnInfo(3, "quandl_code", NpgsqlDbType.Text)]
        public string QuandlCode { get; set; }

        [DbColumnInfo(4, "description", NpgsqlDbType.Text)]
        public string Description { get; set; }
    }
}