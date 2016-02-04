using System;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
{
    public class DeserializeToJsonResponse : IDefineQuery<JsonDataResponse>
    {
        public DeserializeToJsonResponse(string rawResponse)
        {
            RawResponse = rawResponse;
        }

        public string RawResponse { get; private set; }
    }

    public class HandleDeserializeToJsonResponse : IHandleQuery<DeserializeToJsonResponse, JsonDataResponse>
    {
        private readonly IProcessQueries _queries;

        public HandleDeserializeToJsonResponse(IProcessQueries queries)
        {
            _queries = queries;
        }

        public JsonDataResponse Handle(DeserializeToJsonResponse query)
        {
            if (query == null) throw new ArgumentNullException("query");
            return _queries.Execute(new DeserializeToClass<JsonDataResponse>(query.RawResponse));
        }
    }  
    
    public class DeserializeToJsonResponse<TEntity> : IDefineQuery<JsonDataResponse<TEntity>>
        where TEntity : QuandlEntity
    {
        public DeserializeToJsonResponse(string rawResponse)
        {
            RawResponse = rawResponse;
        }

        public string RawResponse { get; private set; }
    }

    public class HandleDeserializeToJsonResponse<TEntity> :
        IHandleQuery<DeserializeToJsonResponse<TEntity>, JsonDataResponse<TEntity>>
        where TEntity : QuandlEntity
    {
        private readonly IProcessQueries _queries;

        public HandleDeserializeToJsonResponse(IProcessQueries queries)
        {
            _queries = queries;
        }

        public JsonDataResponse<TEntity> Handle(DeserializeToJsonResponse<TEntity> query)
        {
            var response = _queries.Execute(new DeserializeToClass<JsonDataResponse<TEntity>>(query.RawResponse));
            response.Entities = _queries.Execute(new MapToEntitiesByDataObjects<TEntity>(response.Data));

            return response;
        }
    }
}