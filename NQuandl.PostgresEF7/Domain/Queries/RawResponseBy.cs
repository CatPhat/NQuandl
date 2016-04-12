using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.PostgresEF7.Api.Entities;
using NQuandl.PostgresEF7.Api.Transactions;
using NQuandl.PostgresEF7.Domain.Entities;

namespace NQuandl.PostgresEF7.Domain.Queries
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