using NQuandl.Api.Persistence;
using NQuandl.Api.Persistence.Entities;

namespace NQuandl.Domain.Persistence.Entities
{
    public class Database : EntityWithId<int>
    {
        public string Name { get; set; }
        public string DatabaseCode { get; set; }
        public string Description { get; set; }
        public int DatasetsCount { get; set; }
        public long Downloads { get; set; }
        public bool Premium { get; set; }
        public string Image { get; set; }
    }
}