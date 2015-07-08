using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
{
    public class RequestJsonResponseV2 : IDefineQuery<Task<JsonResponseV2>>
    {
        public RequestJsonResponseV2(RequestParametersV2 requestParameters)
        {
            RequestParameters = requestParameters;
        }

        public RequestParametersV2 RequestParameters { get; private set; }
    }

    public class HandleRequestJsonResponseV2 : IHandleQuery<RequestJsonResponseV2, Task<JsonResponseV2>>
    {
        private readonly IQuandlJsonClient _client;

        public HandleRequestJsonResponseV2(IQuandlJsonClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            _client = client;
        }

        public async Task<JsonResponseV2> Handle(RequestJsonResponseV2 query)
        {
            return await _client.GetAsync(query.RequestParameters);
        }
    }
}