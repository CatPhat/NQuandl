using System;
using NpgsqlTypes;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Entities
{
    [DbTableName("database_status")]
    public class DatabaseStatus
    {
        [DbColumnInfo(0, "id", NpgsqlDbType.Integer)]
        public int Id { get; set; }

        [DbColumnInfo(1, "database_id", NpgsqlDbType.Integer)]
        public int DatabaseId { get; set; }

        [DbColumnInfo(2, "database_name", NpgsqlDbType.Text)]
        public string DatabaseName { get; set; }

        [DbColumnInfo(3, "datasets_available", NpgsqlDbType.Integer)]
        public int DatasetsAvailable { get; set; }

        [DbColumnInfo(4, "datasets_downloaded", NpgsqlDbType.Integer)]
        public int DatasetsDownloaded { get; set; }

        [DbColumnInfo(5, "dataset_last_checked", NpgsqlDbType.Timestamp)]
        public DateTime DatasetLastChecked { get; set; }

        [DbColumnInfo(6, "database_marked_for_download", NpgsqlDbType.Boolean)]
        public bool DatabaseMarkedForDownload { get; set; }
    }
}