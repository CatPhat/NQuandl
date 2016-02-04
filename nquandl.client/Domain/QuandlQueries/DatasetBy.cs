using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;
using NQuandl.Client.Domain.Queries;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.QuandlQueries
{
    // https://www.quandl.com/api/v3/datasets/WIKI/FB.json
    public class DatasetBy<TEntity> : IDefineQuandlQuery<Task<JsonDatasetResponse<TEntity>>>
        where TEntity : QuandlEntity
    {
        public ResponseFormat ResponseFormat => ResponseFormat.JSON;
        public string ApiVersion => RequestParameterConstants.ApiVersion;

        public int? Limit { get; set; }
        public int? Rows { get; set; }
        public int? ColumnIndex { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Order? Order { get; set; }
        public Collapse? Collapse { get; set; }
        public Transform? Transform { get; set; }
    }

    public class HandleDatasetBy<TEntity> : IHandleQuandlQuery<DatasetBy<TEntity>, Task<JsonDatasetResponse<TEntity>>>
        where TEntity : QuandlEntity
    {
        private readonly IQuandlRestClient _client;
        private readonly IProcessQueries _queries;

        public HandleDatasetBy(IQuandlRestClient client, IProcessQueries queries)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (queries == null) throw new ArgumentNullException(nameof(queries));
            _client = client;
            _queries = queries;
        }

        public async Task<JsonDatasetResponse<TEntity>> Handle(DatasetBy<TEntity> query)
        {
            var entity = (TEntity) Activator.CreateInstance(typeof (TEntity));

            var quandlClientRequestParameters = new QuandlRestClientRequestParameters
            {
                PathSegment =
                    $"{query.ApiVersion}/datasets/{entity.DatabaseCode}/{entity.DatasetCode}.{query.ResponseFormat.GetStringValue()}",
                QueryParameters = query.ToRequestParameterDictionary()
            };

            var rawResponse = await _client.GetStringAsync(quandlClientRequestParameters);
            return _queries.Execute(new DeserializeToJsonResponse<TEntity>(rawResponse));
        }
    }
}