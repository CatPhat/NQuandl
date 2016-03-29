using NQuandl.Api.Persistence;

namespace NQuandl.Domain.Persistence.Entities
{
    public class JsonResponse : EntityWithId<int>
    {
        public string RequestUri { get; set; }
        public string RawJsonResponse { get; set; }
    }
}