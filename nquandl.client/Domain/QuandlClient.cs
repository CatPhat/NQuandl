using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;
using NQuandl.Client.Domain.RequestParameters;

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

        public async Task<string> GetAsync(QuandlClientRequestParameters requestParameters)
        {
            var quandlRestClientParameters = new QuandlRestClientRequestParameters
            {
                PathSegment = requestParameters.PathSegmentParameters.ToPathSegment(),
                QueryParameters = requestParameters.RequestParametersDictionary
            };
            return await _client.GetStringAsync(quandlRestClientParameters);
        }
    }
}