using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.Client.Domain.Queries
{
    public class RequestString : IDefineQuery<Task<string>>
    {
        public RequestString(RequestParametersV2 requestParametersV2)
        {
            RequestParametersV2 = requestParametersV2;
        }

        public RequestParametersV2 RequestParametersV2 { get; private set; }
    }

    public class HandleRequestString : IHandleQuery<RequestString, Task<string>>
    {
        private readonly IQuandlJsonClient _client;

        public HandleRequestString(IQuandlJsonClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            _client = client;
        }

        public async Task<string> Handle(RequestString query)
        {
            return await _client.GetStringAsync(query.RequestParametersV2);
        }
    }
}