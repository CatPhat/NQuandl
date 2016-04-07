using System;
using NQuandl.Domain.Persistence.Api.Entities;

namespace NQuandl.Domain.Persistence.Domain.Entities
{
    public class RawResponse : EntityWithId<int>
    {
        public string RequestUri { get; set; }
        public string ResponseContent { get; set; }
        public DateTime CreationDate { get; set; }
    }
}