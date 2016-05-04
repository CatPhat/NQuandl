﻿using NpgsqlTypes;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Entities
{
    [DbTableName("dataset_countries")]
    public class DatasetCountry : DbEntityWithSerialId
    {
        [DbColumnInfo(0, "id", NpgsqlDbType.Integer)]
        public override int Id { get; set; }

        [DbColumnInfo(1, "dataset_id", NpgsqlDbType.Integer)]
        public int DatasetId { get; set; }

        [DbColumnInfo(2, "iso31661alpha3", NpgsqlDbType.Text)]
        public string Iso31661Alpha3 { get; set; }

        [DbColumnInfo(3, "Association", NpgsqlDbType.Text)]
        public string Association { get; set; }
    }
}