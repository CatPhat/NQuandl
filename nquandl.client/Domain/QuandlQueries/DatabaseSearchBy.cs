using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;
using NQuandl.Client.Domain.Queries;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.QuandlQueries
{
    // https://www.quandl.com/api/v3/databases.json
    public class DatabaseSearchBy : IDefineQuandlQuery<Task<JsonDatabaseSearchResponse>>
    {
        public DatabaseSearchBy(string query)
        {
            Query = query;
        }

        public ResponseFormat ResponseFormat => ResponseFormat.JSON;

        // optional
        public string Query { get; private set; }
        public int? PerPage { get; set; }
        public int? Page { get; set; }
        public string ApiVersion => RequestParameterConstants.ApiVersion;
    }

    public class HandleDatabaseSearchBy : IHandleQuandlQuery<DatabaseSearchBy, Task<JsonDatabaseSearchResponse>>
    {
        private readonly IQuandlRestClient _client;
        private readonly IProcessQueries _queries;

        public HandleDatabaseSearchBy(IQuandlRestClient client, IProcessQueries queries)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (queries == null) throw new ArgumentNullException(nameof(queries));
            _client = client;
            _queries = queries;
        }

        public async Task<JsonDatabaseSearchResponse> Handle(DatabaseSearchBy query)
        {
            var quandlClientRequestParameters = new QuandlRestClientRequestParameters
            {
                PathSegment = $"{query.ApiVersion}/databases.{query.ResponseFormat.GetStringValue()}",
                QueryParameters = query.ToRequestParameterDictionary()
            };

            var rawResponse = await _client.GetStringAsync(quandlClientRequestParameters);
            var response = _queries.Execute(new DeserializeToClass<JsonDatabaseSearchResponse>(rawResponse));
            return response;
        }
    }
}