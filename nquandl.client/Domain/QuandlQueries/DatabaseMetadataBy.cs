using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;
using NQuandl.Client.Domain.Queries;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.QuandlQueries
{
    // https://www.quandl.com/api/v3/databases/WIKI.json
    public class DatabaseMetadataBy : IDefineQuandlQuery<Task<JsonDatabaseMetadataResponse>>
    {
        public DatabaseMetadataBy(string databaseCode, string datasetCode)
        {
            DatabaseCode = databaseCode;
            DatasetCode = datasetCode;
        }

        public string DatabaseCode { get; }
        public string DatasetCode { get; private set; }
        public ResponseFormat ResponseFormat => ResponseFormat.JSON;

        public string ApiVersion => RequestParameterConstants.ApiVersion;
    }

    public class HandleDatabaseMetadataBy : IHandleQuandlQuery<DatabaseMetadataBy, Task<JsonDatabaseMetadataResponse>>
    {
        private readonly IQuandlRestClient _client;
        private readonly IProcessQueries _queries;

        public HandleDatabaseMetadataBy(IQuandlRestClient client, IProcessQueries queries)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (queries == null) throw new ArgumentNullException(nameof(queries));
            _client = client;
            _queries = queries;
        }

        public async Task<JsonDatabaseMetadataResponse> Handle(DatabaseMetadataBy query)
        {
            var quandlClientRequestParameters = new QuandlRestClientRequestParameters
            {
                PathSegment =
                    $"{query.ApiVersion}/databases/{query.DatabaseCode}.{query.ResponseFormat.GetStringValue()}",
                QueryParameters = query.ToRequestParameterDictionary()
            };

            var rawResponse = await _client.GetStringAsync(quandlClientRequestParameters);
            var response = _queries.Execute(new DeserializeToClass<JsonDatabaseMetadataResponse>(rawResponse));
            return response;
        }
    }
}