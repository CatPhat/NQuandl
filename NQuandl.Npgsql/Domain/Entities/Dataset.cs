using System;
using NpgsqlTypes;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Entities
{
    [DbTableName("datasets")]
    public class Dataset
    {
        [DbColumnInfo(0, "id", NpgsqlDbType.Integer)]
        public int Id { get; set; }

        [DbColumnInfo(1, "code", NpgsqlDbType.Text)]
        public string Code { get; set; }

        [DbColumnInfo(2, "database_code", NpgsqlDbType.Text)]
        public string DatabaseCode { get; set; }

        [DbColumnInfo(3, "database_id", NpgsqlDbType.Integer)]
        public int? DatabaseId { get; set; }

        [DbColumnInfo(4, "description", NpgsqlDbType.Text)]
        public string Description{ get; set; }

        [DbColumnInfo(5, "end_date", NpgsqlDbType.Date)]
        public DateTime? EndDate { get; set; }

        [DbColumnInfo(6, "frequency", NpgsqlDbType.Text)]
        public string Frequency { get; set; }

        [DbColumnInfo(7, "name", NpgsqlDbType.Text)]
        public string Name { get; set; }

        [DbColumnInfo(8, "refreshed_at", NpgsqlDbType.Date)]
        public DateTime? RefreshedAt { get; set; }

        [DbColumnInfo(9, "start_date", NpgsqlDbType.Date)]
        public DateTime? StartDate { get; set; }

        [DbColumnInfo(10, "data", NpgsqlDbType.Jsonb)]
        public dynamic Data { get; set; }
    }
}