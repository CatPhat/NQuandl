using NpgsqlTypes;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Entities
{
    [DbTableName("databases")]
    public class Database
    {
        [DbColumnInfo(0, "id", NpgsqlDbType.Integer)]
        public int Id { get; set; }

        [DbColumnInfo(1, "database_code", NpgsqlDbType.Text)]
        public string DatabaseCode { get; set; }

        [DbColumnInfo(2, "datasets_count", NpgsqlDbType.Integer)]
        public int DatasetsCount { get; set; }

        [DbColumnInfo(3, "description", NpgsqlDbType.Text)]
        public string Description { get; set; }

        [DbColumnInfo(4, "downloads", NpgsqlDbType.Bigint)]
        public long Downloads { get; set; }

        [DbColumnInfo(5, "image", NpgsqlDbType.Text)]
        public string Image { get; set; }

        [DbColumnInfo(6, "name", NpgsqlDbType.Text)]
        public string Name { get; set; }

        [DbColumnInfo(7, "premium", NpgsqlDbType.Boolean)]
        public bool Premium { get; set; }
    }
}