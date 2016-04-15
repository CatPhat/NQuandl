using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Entities
{
    [DbTableName("databases")]
    public class Database
    {
        [DbColumnInfo(0, "id")]
        public int Id { get; set; }

        [DbColumnInfo(1, "database_code")]
        public string DatabaseCode { get; set; }

        [DbColumnInfo(2, "datasets_count")]
        public int DatasetsCount { get; set; }

        [DbColumnInfo(3, "description")]
        public string Description { get; set; }

        [DbColumnInfo(4, "downloads")]
        public long Downloads { get; set; }

        [DbColumnInfo(5, "image")]
        public string Image { get; set; }

        [DbColumnInfo(6, "name")]
        public string Name { get; set; }

        [DbColumnInfo(7, "premium")]
        public bool Premium { get; set; }
    }
}