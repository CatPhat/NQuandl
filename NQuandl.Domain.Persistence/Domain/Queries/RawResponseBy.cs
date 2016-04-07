using System;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Domain.Persistence.Api.Entities;
using NQuandl.Domain.Persistence.Api.Transactions;
using NQuandl.Domain.Persistence.Domain.Entities;

namespace NQuandl.Domain.Persistence.Domain.Queries
{
    public class RawResponseBy : IDefineQuery<Task<RawResponse>>
    {
        public RawResponseBy(string requestUri)
        {
            RequestUri = requestUri;
        }

        public string RequestUri { get; }
    }

    public class HandleRawResponseBy : IHandleQuery<RawResponseBy, Task<RawResponse>>
    {
        private readonly IReadEntities _entities;

        public HandleRawResponseBy([NotNull] IReadEntities entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            _entities = entities;
        }

        public Task<RawResponse> Handle(RawResponseBy query)
        {
            var entity = _entities.Query<RawResponse>().FirstOrDefault(x => x.RequestUri == query.RequestUri);
            return Task.FromResult(entity);
        }
    }
}