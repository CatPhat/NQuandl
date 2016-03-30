using System;
using NQuandl.Api.Persistence;

namespace NQuandl.Domain.Persistence.Entities
{
    public class Dataset : EntityWithId<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string DatabaseCode { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public dynamic Data { get; set; }
    }
}