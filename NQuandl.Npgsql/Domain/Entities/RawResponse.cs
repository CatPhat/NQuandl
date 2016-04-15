using System;
using NQuandl.Npgsql.Services.Helpers;

namespace NQuandl.Npgsql.Domain.Entities
{
    [DbTableName("raw_response")]
    public class RawResponse
    {
        [DbColumnInfo(0, "id")]
        public int Id { get; set; }

        [DbColumnInfo(1, "creation_date")]
        public DateTime CreationDate { get; set; }

        [DbColumnInfo(2, "request_uri")]
        public string RequestUri { get; set; }

        [DbColumnInfo(3, "response_content")]
        public string ResponseContent { get; set; }
    }
}