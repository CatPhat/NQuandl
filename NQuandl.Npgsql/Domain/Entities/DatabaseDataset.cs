using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Entities
{
    [DbTableName("database_datasets")]
    public class DatabaseDataset
    {
        [DbColumnInfo(0, "id")]
        public int Id { get; set; }

        [DbColumnInfo(1, "database_code")]
        public string DatabaseCode { get; set; }

        [DbColumnInfo(2, "dataset_code")]
        public string DatasetCode { get; set; }

        [DbColumnInfo(3, "quandl_code")]
        public string QuandlCode { get; set; }

        [DbColumnInfo(4, "description")]
        public string Description { get; set; }
    }
}