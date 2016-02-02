using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.Entities;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
{
    public class RequestJsonFredGdp : IDefineQuery<Task<JsonResponse>>
    {
        public RequestParameters.RequestParameters RequestParameters { get; set; }
    }

    public class HandleRequestJsonFredGdp : IHandleQuery<RequestJsonFredGdp, Task<JsonResponse>>
    {
        private readonly IQuandlJsonClient _client;

        public HandleRequestJsonFredGdp(IQuandlJsonClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            _client = client;
        }

        public async Task<JsonResponse> Handle(RequestJsonFredGdp query)
        {
            return await _client.GetAsync<FredGdp>(query.RequestParameters);
        }
    }
}