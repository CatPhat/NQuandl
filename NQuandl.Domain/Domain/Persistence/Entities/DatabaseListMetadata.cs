using NQuandl.Api.Persistence;

namespace NQuandl.Domain.Persistence.Entities
{
    public class DatabaseListMetadata : EntityWithId<int>
    {
        public int CurrentPage { get; set; }
        public int NextPage { get; set; }
        public int PreviousPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PerPage { get; set; }
        public int CurrentFirstItem { get; set; }
        public int CurrentLastItem { get; set; }
    }
}