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
    public class DatabaseListBy : IDefineQuandlQuery<Task<JsonDatabaseListResponse>>
    {
        public int? PerPage { get; set; }
        public int? Page { get; set; }
        public ResponseFormat ResponseFormat => ResponseFormat.JSON;

        public string ApiVersion => RequestParameterConstants.ApiVersion;
    }

    public class HandleDatabaseListBy : IHandleQuandlQuery<DatabaseListBy, Task<JsonDatabaseListResponse>>
    {
        private readonly IQuandlRestClient _client;
        private readonly IProcessQueries _queries;

        public HandleDatabaseListBy(IQuandlRestClient client, IProcessQueries queries)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (queries == null) throw new ArgumentNullException(nameof(queries));
            _client = client;
            _queries = queries;
        }

        public async Task<JsonDatabaseListResponse> Handle(DatabaseListBy query)
        {
            var quandlClientRequestParameters = new QuandlRestClientRequestParameters
            {
                PathSegment = $"{query.ApiVersion}/databases.{query.ResponseFormat.GetStringValue()}",
                QueryParameters = query.ToRequestParameterDictionary()
            };

            var rawResponse = await _client.GetStringAsync(quandlClientRequestParameters);
            var response = _queries.Execute(new DeserializeToClass<JsonDatabaseListResponse>(rawResponse));
            return response;
        }
    }
}