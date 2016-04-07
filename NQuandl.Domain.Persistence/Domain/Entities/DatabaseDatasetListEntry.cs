using NQuandl.Domain.Persistence.Api.Entities;

namespace NQuandl.Domain.Persistence.Domain.Entities
{
    public class DatabaseDatasetListEntry : EntityWithId<int>
    {
        public string QuandlCode { get; set; }
        public string DatabaseCode { get; set; }
        public string DatasetCode { get; set; }
        public string Description { get; set; }
    }
}