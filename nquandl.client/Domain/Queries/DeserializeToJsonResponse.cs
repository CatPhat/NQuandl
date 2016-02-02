using System;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
{
    public class DeserializeToJsonResponse : IDefineQuery<JsonResponse>
    {
        public DeserializeToJsonResponse(string rawResponse)
        {
            RawResponse = rawResponse;
        }

        public string RawResponse { get; private set; }
    }

    public class HandleDeserializeToJsonResponse : IHandleQuery<DeserializeToJsonResponse, JsonResponse>
    {
        private readonly IProcessQueries _queries;

        public HandleDeserializeToJsonResponse(IProcessQueries queries)
        {
            _queries = queries;
        }

        public JsonResponse Handle(DeserializeToJsonResponse query)
        {
            if (query == null) throw new ArgumentNullException("query");
            return _queries.Execute(new DeserializeToClass<JsonResponse>(query.RawResponse));
        }
    }  
    
    public class DeserializeToJsonResponseV1<TEntity> : IDefineQuery<JsonResponse>
        where TEntity : QuandlEntity
    {
        public DeserializeToJsonResponseV1(string rawResponse)
        {
            RawResponse = rawResponse;
        }

        public string RawResponse { get; private set; }
    }

    public class HandleDeserializeToJsonResponseV1<TEntity> :
        IHandleQuery<DeserializeToJsonResponseV1<TEntity>, JsonResponse>
        where TEntity : QuandlEntity
    {
        private readonly IProcessQueries _queries;

        public HandleDeserializeToJsonResponseV1(IProcessQueries queries)
        {
            _queries = queries;
        }

        public JsonResponse Handle(DeserializeToJsonResponseV1<TEntity> query)
        {
            var response = _queries.Execute(new DeserializeToClass<JsonResponse>(query.RawResponse));
            response.Entities = _queries.Execute(new MapToEntitiesByDataObjects<TEntity>(response.Data));

            return response;
        }
    }
}