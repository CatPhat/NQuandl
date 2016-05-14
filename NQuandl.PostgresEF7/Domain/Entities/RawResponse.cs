using System;
using NQuandl.PostgresEF7.Api.Entities;

namespace NQuandl.PostgresEF7.Domain.Entities
{
    public class RawResponse : EntityWithId<int>
    {
        public string RequestUri { get; set; }
        public string ResponseContent { get; set; }
        public DateTime CreationDate { get; set; }
    }
}