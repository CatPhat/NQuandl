using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
{
    // https://www.quandl.com/api/v3/databases.json
    public class DatabaseSearchBy : IDefineQuery<Task<DatabaseSearch>>
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

    public class HandleDatabaseSearchBy : IHandleQuery<DatabaseSearchBy, Task<DatabaseSearch>>
    {
        private readonly IQuandlClient _client;
        private readonly IProcessQueries _queries;

        public HandleDatabaseSearchBy(IQuandlClient client, IProcessQueries queries)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (queries == null) throw new ArgumentNullException(nameof(queries));
            _client = client;
            _queries = queries;
        }

        public async Task<DatabaseSearch> Handle(DatabaseSearchBy query)
        {
            var quandlClientRequestParameters = new QuandlClientRequestParameters
            {
                PathSegment = $"{query.ApiVersion}/databases.{query.ResponseFormat.GetStringValue()}",
                QueryParameters = query.ToRequestParameterDictionary()
            };

            return await _queries.Execute(new QuandlQueryBy<DatabaseSearch>(quandlClientRequestParameters));
        }
    }
}