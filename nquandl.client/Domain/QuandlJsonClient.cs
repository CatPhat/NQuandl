using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;
using NQuandl.Client.Domain.Queries;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain
{
    public class QuandlJsonClient : IQuandlJsonClient
    {
        private readonly IQuandlRestClient _client;
        private readonly IProcessQueries _queries;

        public QuandlJsonClient(IQuandlRestClient client, IProcessQueries queries)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (queries == null) throw new ArgumentNullException("queries");
            _client = client;
            _queries = queries;
        }
        
        public async Task<JsonDatabaseListResponse> GetAsync(DatabaseListRequestParameters requestParameters)
        {
            var quandlClientRequestParameters = new QuandlRestClientRequestParameters
            {
                PathSegment = requestParameters.ToPathSegment(),
                QueryParameters = requestParameters.ToRequestParameterDictionary()
            };

            var rawResponse = await _client.GetStringAsync(quandlClientRequestParameters);
            var response = _queries.Execute(new DeserializeToClass<JsonDatabaseListResponse>(rawResponse));
            return response;
        }

        public async Task<JsonDatabaseMetadataResponse> GetAsync(DatabaseMetadataRequestParameters requestParameters)
        {
            var quandlClientRequestParameters = new QuandlRestClientRequestParameters
            {
                PathSegment = requestParameters.ToPathSegment(),
                QueryParameters = requestParameters.ToRequestParameterDictionary()
            };

            var rawResponse = await _client.GetStringAsync(quandlClientRequestParameters);
            var response = _queries.Execute(new DeserializeToClass<JsonDatabaseMetadataResponse>(rawResponse));
            return response;
        }

        public async Task<JsonDatabaseSearchResponse> GetAsync(DatabaseSearchRequestParameters requestParameters)
        {
            var quandlClientRequestParameters = new QuandlRestClientRequestParameters
            {
                PathSegment = requestParameters.ToPathSegment(),
                QueryParameters = requestParameters.ToRequestParameterDictionary()
            };

            var rawResponse = await _client.GetStringAsync(quandlClientRequestParameters);
            var response = _queries.Execute(new DeserializeToClass<JsonDatabaseSearchResponse>(rawResponse));
            return response;
        }
    }
}