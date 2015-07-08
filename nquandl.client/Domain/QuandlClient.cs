using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;

namespace NQuandl.Client.Domain
{
    public class QuandlClient : IQuandlClient
    {
        private readonly IQuandlRestClient _client;

        public QuandlClient(IQuandlRestClient client)
        {
            if (client == null) throw new ArgumentNullException("client");
            _client = client;
        }

        public async Task<string> GetAsync(QuandlRequestParameters requestParameters)
        {
            return await _client.DoGetRequestAsync(requestParameters);
        }
    }
}