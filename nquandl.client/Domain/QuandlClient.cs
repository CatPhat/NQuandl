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

        public async Task<string> GetAsync(QuandlClientRequestParametersV1 requestParameters)
        {
            var quandlRestClientParameters = new QuandlRestClientRequestParameters
            {
                PathSegment = requestParameters.PathSegmentParameters.ToPathSegment(),
                QueryParameters = requestParameters.RequestParameters.ToQueryParameterDictionary()
            };
            return await _client.GetStringAsync(quandlRestClientParameters);
        }

        public async Task<string> GetAsync(QuandlClientRequestParametersV2 requestParameters)
        {
            var quandlRestClientParameters = new QuandlRestClientRequestParameters
            {
                PathSegment = requestParameters.PathSegmentParameters.ToPathSegment(),
                QueryParameters = requestParameters.RequestParameters.ToQueryParameterDictionary()
            };
            return await _client.GetStringAsync(quandlRestClientParameters);
        }
    }
}