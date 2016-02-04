using System;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
{
    public class DeserializeToJsonResponse : IDefineQuery<JsonDatasetResponse>
    {
        public DeserializeToJsonResponse(string rawResponse)
        {
            RawResponse = rawResponse;
        }

        public string RawResponse { get; private set; }
    }

    public class HandleDeserializeToJsonResponse : IHandleQuery<DeserializeToJsonResponse, JsonDatasetResponse>
    {
        private readonly IProcessQueries _queries;

        public HandleDeserializeToJsonResponse(IProcessQueries queries)
        {
            _queries = queries;
        }

        public JsonDatasetResponse Handle(DeserializeToJsonResponse query)
        {
            if (query == null) throw new ArgumentNullException("query");
            return _queries.Execute(new DeserializeToClass<JsonDatasetResponse>(query.RawResponse));
        }
    }  
    
    public class DeserializeToJsonResponse<TEntity> : IDefineQuery<JsonDatasetResponse<TEntity>>
        where TEntity : QuandlEntity
    {
        public DeserializeToJsonResponse(string rawResponse)
        {
            RawResponse = rawResponse;
        }

        public string RawResponse { get; private set; }
    }

    public class HandleDeserializeToJsonResponse<TEntity> :
        IHandleQuery<DeserializeToJsonResponse<TEntity>, JsonDatasetResponse<TEntity>>
        where TEntity : QuandlEntity
    {
        private readonly IProcessQueries _queries;

        public HandleDeserializeToJsonResponse(IProcessQueries queries)
        {
            _queries = queries;
        }

        public JsonDatasetResponse<TEntity> Handle(DeserializeToJsonResponse<TEntity> query)
        {
            var response = _queries.Execute(new DeserializeToClass<JsonDatasetResponse<TEntity>>(query.RawResponse));
            response.Entities = _queries.Execute(new MapToEntitiesByDataObjects<TEntity>(response.dataset.data));

            return response;
        }
    }
}