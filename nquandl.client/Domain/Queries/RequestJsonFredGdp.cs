using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.Entities;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
{
    public class RequestJsonFredGdp : IDefineQuery<Task<JsonResponseV1<FredGdp>>>
    {
        public RequestParametersV1 RequestParametersV1 { get; set; }
    }

    public class HandleRequestJsonFredGdp : IHandleQuery<RequestJsonFredGdp, Task<JsonResponseV1<FredGdp>>>
    {
        private readonly IQuandlJsonClient _client;

        public HandleRequestJsonFredGdp(IQuandlJsonClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            _client = client;
        }

        public async Task<JsonResponseV1<FredGdp>> Handle(RequestJsonFredGdp query)
        {
            return await _client.GetAsync<FredGdp>(query.RequestParametersV1);
        }
    }
}