using System;
using NpgsqlTypes;
using NQuandl.Npgsql.Api.Entities;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Entities
{
    [DbTableName("raw_responses")]
    public class RawResponse : DbEntity
    {
        [DbColumnInfo(0, "id", NpgsqlDbType.Integer, isStoreGenerated: true)]
        public int Id { get; set; }

        [DbColumnInfo(1, "creation_date", NpgsqlDbType.Date)]
        public DateTime CreationDate { get; set; }

        [DbColumnInfo(2, "request_uri", NpgsqlDbType.Text)]
        public string RequestUri { get; set; }

        [DbColumnInfo(3, "response_content", NpgsqlDbType.Jsonb)]
        public string ResponseContent { get; set; }
    }
}