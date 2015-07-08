using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
{
    public class RequestJsonResponseV1ByEntity<TEntity> : IDefineQuery<Task<JsonResponseV1<TEntity>>>
        where TEntity : QuandlEntity
    {
        public RequestJsonResponseV1ByEntity(RequestParametersV1 queryParameters)
        {
            if (queryParameters == null) throw new ArgumentNullException("queryParameters");
            QueryParameters = queryParameters;
        }

        public RequestParametersV1 QueryParameters { get; private set; }
    }

    public class HandleRequestJsonResponseV1ByEntity<TEntity> :
        IHandleQuery<RequestJsonResponseV1ByEntity<TEntity>, Task<JsonResponseV1<TEntity>>> where TEntity : QuandlEntity
    {
        private readonly IQuandlJsonClient _client;

        public HandleRequestJsonResponseV1ByEntity(IQuandlJsonClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            _client = client;
        }

        public async Task<JsonResponseV1<TEntity>> Handle(RequestJsonResponseV1ByEntity<TEntity> query)
        {
            if (query == null) throw new ArgumentNullException("query");
            return await _client.GetAsync<TEntity>(query.QueryParameters);
        }
    }
}