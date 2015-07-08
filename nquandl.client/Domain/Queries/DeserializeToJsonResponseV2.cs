using System;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
{
    public class DeserializeToJsonResponseV2 : IDefineQuery<JsonResponseV2>
    {
        public DeserializeToJsonResponseV2(string rawResponse)
        {
            RawResponse = rawResponse;
        }

        public string RawResponse { get; private set; }
    }

    public class HandleDeserializeToJsonResponseV2 : IHandleQuery<DeserializeToJsonResponseV2, JsonResponseV2>
    {
        private readonly IProcessQueries _queries;

        public HandleDeserializeToJsonResponseV2(IProcessQueries queries)
        {
            _queries = queries;
        }

        public JsonResponseV2 Handle(DeserializeToJsonResponseV2 query)
        {
            if (query == null) throw new ArgumentNullException("query");
            return _queries.Execute(new DeserializeToClass<JsonResponseV2>(query.RawResponse));
        }
    }
}