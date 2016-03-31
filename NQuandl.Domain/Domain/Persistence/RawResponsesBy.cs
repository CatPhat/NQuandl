using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Persistence.Entities;
using NQuandl.Api.Persistence.Transactions;
using NQuandl.Domain.Persistence.Entities;

namespace NQuandl.Domain.Persistence
{
    public class RawResponsesBy : BaseEntitiesQuery<RawResponse>, IDefineQuery<Task<IEnumerable<RawResponse>>>
    {

    }

    public class HandleRawResponsesBy : IHandleQuery<RawResponsesBy, Task<IEnumerable<RawResponse>>>
    {
        private readonly IReadEntities _entities;

        public HandleRawResponsesBy([NotNull] IReadEntities entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            _entities = entities;
        }

        public async Task<IEnumerable<RawResponse>> Handle(RawResponsesBy query)
        {
            var result = _entities.Query<RawResponse>().ToList();
            return await Task.FromResult(result);
        }
    }
}
