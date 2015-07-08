using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

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
                QueryParameters = requestParameters.QueryParameters.ToQueryParameterDictionary()
            };
            return await _client.DoGetRequestAsync(quandlRestClientParameters);
        }
    }
}