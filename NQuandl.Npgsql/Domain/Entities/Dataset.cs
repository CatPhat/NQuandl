using System;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Entities
{
    [DbTableName("datasets")]
    public class Dataset
    {
        [DbColumnInfo(0, "id")]
        public int Id { get; set; }

        [DbColumnInfo(1, "code")]
        public string Code { get; set; }

        [DbColumnInfo(2, "data")]
        public string Data { get; set; }

        [DbColumnInfo(3, "database_code")]
        public string DatabaseCode { get; set; }

        [DbColumnInfo(4, "database_id")]
        public int DatabaseId { get; set; }

        [DbColumnInfo(5, "description")]
        public string Description { get; set; }

        [DbColumnInfo(6, "end_date")]
        public DateTime EndDate { get; set; }

        [DbColumnInfo(7, "frequency")]
        public string Frequency { get; set; }

        [DbColumnInfo(8, "name")]
        public string Name { get; set; }

        [DbColumnInfo(9, "refreshed_at")]
        public DateTime RefreshedAt { get; set; }

        [DbColumnInfo(10, "start_date")]
        public DateTime StartDate { get; set; }
    }
}