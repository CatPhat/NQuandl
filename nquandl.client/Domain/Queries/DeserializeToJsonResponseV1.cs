using System;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
{
    public class DeserializeToJsonResponseV1 : IDefineQuery<JsonResponseV1>
    {
        public DeserializeToJsonResponseV1(string rawResponse)
        {
            RawResponse = rawResponse;
        }

        public string RawResponse { get; private set; }
    }

    public class HandleDeserializeToJsonResponseV1 : IHandleQuery<DeserializeToJsonResponseV1, JsonResponseV1>
    {
        private readonly IProcessQueries _queries;

        public HandleDeserializeToJsonResponseV1(IProcessQueries queries)
        {
            _queries = queries;
        }

        public JsonResponseV1 Handle(DeserializeToJsonResponseV1 query)
        {
            if (query == null) throw new ArgumentNullException("query");
            return _queries.Execute(new DeserializeToClass<JsonResponseV1>(query.RawResponse));
        }
    }

    public class DeserializeToJsonResponseV1<TEntity> : IDefineQuery<JsonResponseV1<TEntity>>
        where TEntity : QuandlEntity
    {
        public DeserializeToJsonResponseV1(string rawResponse)
        {
            RawResponse = rawResponse;
        }

        public string RawResponse { get; private set; }
    }

    public class HandleDeserializeToJsonResponseV1<TEntity> :
        IHandleQuery<DeserializeToJsonResponseV1<TEntity>, JsonResponseV1<TEntity>>
        where TEntity : QuandlEntity
    {
        private readonly IProcessQueries _queries;

        public HandleDeserializeToJsonResponseV1(IProcessQueries queries)
        {
            _queries = queries;
        }

        public JsonResponseV1<TEntity> Handle(DeserializeToJsonResponseV1<TEntity> query)
        {
            var response = _queries.Execute(new DeserializeToClass<JsonResponseV1<TEntity>>(query.RawResponse));
            response.Entities = _queries.Execute(new MapToEntitiesByDataObjects<TEntity>(response.Data));

            return response;
        }
    }
}