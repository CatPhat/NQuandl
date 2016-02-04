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


        public async Task<JsonDataResponse<TEntity>> GetAsync<TEntity>(DataRequestParameters<TEntity> requestParameters)
            where TEntity : QuandlEntity
        {
            var entity = (TEntity) Activator.CreateInstance(typeof (TEntity));
            var parameters = new DataRequestParameters
            {
                Collapse = requestParameters.Collapse,
                ColumnIndex = requestParameters.ColumnIndex,
                DatabaseCode = entity.DatabaseCode,
                DatasetCode = entity.DatasetCode,
                EndDate = requestParameters.EndDate,
                StartDate = requestParameters.StartDate,
                ApiKey = "XXXXXXX",
                Limit = requestParameters.Limit,
                Order = requestParameters.Order,
                Rows = requestParameters.Rows
            };

            var pathSegmentParameters = new PathSegmentParameters
            {
                ApiVersion = RequestParameterConstants.ApiVersion,
                DatabaseCode = parameters.DatabaseCode,
                DatasetCode = parameters.DatasetCode,
                ResponseFormat = ResponseFormat.JSON.GetStringValue()
            };

            var quandlClientRequestParameters = new QuandlRestClientRequestParameters
            {
                PathSegment = pathSegmentParameters.ToPathSegment(),
                QueryParameters = parameters.ToRequestParameterDictionary()
            };

            var rawResponse = await _client.GetStringAsync(quandlClientRequestParameters);
            return _queries.Execute(new DeserializeToJsonResponse<TEntity>(rawResponse));
        }
    }
}