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
        private readonly IQuandlClient _client;
        private readonly IProcessQueries _queries;

        public QuandlJsonClient(IQuandlClient client, IProcessQueries queries)
        {
            if (client == null) throw new ArgumentNullException("client");
            if (queries == null) throw new ArgumentNullException("queries");
            _client = client;
            _queries = queries;
        }

        public async Task<JsonResponseV1<TEntity>> GetAsync<TEntity>(RequestParameters.RequestParameters requestParameters)
            where TEntity : QuandlEntity
        {
            var quandlClientRequestParameters = GetQuandlClientRequestParameters<TEntity>(requestParameters);
            var rawResponse = await _client.GetAsync(quandlClientRequestParameters);
            return _queries.Execute(new DeserializeToJsonResponseV1<TEntity>(rawResponse));
        }

        public async Task<string> GetStringAsync<TEntity>(RequestParameters.RequestParameters requestParameters) where TEntity : QuandlEntity
        {
            var quandlClientRequestParameters = GetQuandlClientRequestParameters<TEntity>(requestParameters);
            var rawResponse = await _client.GetAsync(quandlClientRequestParameters);
            return rawResponse;
        }

        private QuandlClientRequestParameters GetQuandlClientRequestParameters<TEntity>(RequestParameters.RequestParameters requestParameters) where TEntity : QuandlEntity
        {
            var quandlCode = _queries.Execute(new GetQuandlCodeByEntity<TEntity>());
            var quandlClientRequestParameters = new QuandlClientRequestParameters
            {
                PathSegmentParameters = GetPathSegmentParametersV1(quandlCode),
                RequestParameters = requestParameters,
                Format = ResponseFormat.JSON
            };
            return quandlClientRequestParameters;
        }

        private QuandlClientRequestParametersV2 GetQuandlClientRequestParameters(RequestParametersV2 requestParameters)
        {
            var quandlClientRequestParameters = new QuandlClientRequestParametersV2
            {
                PathSegmentParameters = GetPathSegmentParametersV2(),
                RequestParameters = requestParameters,
                Format = ResponseFormat.JSON
            };
            return quandlClientRequestParameters;
        }

        private PathSegmentParametersV2 GetPathSegmentParametersV2()
        {
            var pathSegmentParameters = new PathSegmentParametersV2
            {
                ApiVersion = RequestParameterConstants.ApiVersion2,
                ResponseFormat = ResponseFormat.JSON.ToString()
            };

            return pathSegmentParameters;
        }

        private PathSegmentParametersV1 GetPathSegmentParametersV1(string quandlCode)
        {
            var pathSegmentParameters = new PathSegmentParametersV1
            {
                ApiVersion = RequestParameterConstants.ApiVersion1,
                QuandlCode = quandlCode,
                ResponseFormat = ResponseFormat.JSON.ToString()
            };

            return pathSegmentParameters;
        }
    }
}