using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
{
    public class RequestJsonResponseByEntity<TEntity> : IDefineQuery<Task<JsonResponse>>
        where TEntity : QuandlEntity
    {
        public RequestJsonResponseByEntity(RequestParameters.RequestParameters queryParameters)
        {
            if (queryParameters == null) throw new ArgumentNullException("queryParameters");
            QueryParameters = queryParameters;
        }

        public RequestParameters.RequestParameters QueryParameters { get; private set; }
    }

    public class HandleRequestJsonResponseByEntity<TEntity> :
        IHandleQuery<RequestJsonResponseByEntity<TEntity>, Task<JsonResponse>> where TEntity : QuandlEntity
    {
        private readonly IQuandlJsonClient _client;

        public HandleRequestJsonResponseByEntity(IQuandlJsonClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            _client = client;
        }

        public async Task<JsonResponse> Handle(RequestJsonResponseByEntity<TEntity> query)
        {
            if (query == null) throw new ArgumentNullException("query");
            return await _client.GetAsync<TEntity>(query.QueryParameters);
        }
    }
}