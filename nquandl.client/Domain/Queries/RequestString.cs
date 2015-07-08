using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.Client.Domain.Queries
{
    public class RequestString : IDefineQuery<Task<string>>
    {
        public RequestString(RequestParametersV1 requestParametersV1)
        {
            RequestParametersV1 = requestParametersV1;
        }

        public RequestString(RequestParametersV2 requestParametersV2)
        {
            RequestParametersV2 = requestParametersV2;
        }

        public RequestParametersV1 RequestParametersV1 { get; private set; }
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
            if (query.RequestParametersV1 != null)
            {
                return await _client.GetStringAsync(query.RequestParametersV1);
            }

            if (query.RequestParametersV2 != null)
            {
                return await _client.GetStringAsync(query.RequestParametersV2);
            }
           
        }
    }
}