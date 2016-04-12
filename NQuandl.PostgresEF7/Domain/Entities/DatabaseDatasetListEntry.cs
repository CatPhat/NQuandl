using NQuandl.PostgresEF7.Api.Entities;

namespace NQuandl.PostgresEF7.Domain.Entities
{
    public class DatabaseDatasetListEntry : EntityWithId<int>
    {
        public string QuandlCode { get; set; }
        public string DatabaseCode { get; set; }
        public string DatasetCode { get; set; }
        public string Description { get; set; }
    }
}