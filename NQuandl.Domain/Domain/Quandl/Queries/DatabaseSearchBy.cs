using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Quandl;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Api.Transactions;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Domain.Quandl.Queries
{
    // https://www.quandl.com/api/v3/databases.json
    public class DatabaseSearchBy : IDefineQuery<Task<DatabaseSearch>>
    {
        public ResponseFormat ResponseFormat => ResponseFormat.JSON;

        // optional
        public string Query { get; set; }
        public int? PerPage { get; set; }
        public int? Page { get; set; }
        public string ApiVersion => RequestParameterConstants.ApiVersion;
    }

    public class HandleDatabaseSearchBy : IHandleQuery<DatabaseSearchBy, Task<DatabaseSearch>>
    {
        private readonly IQuandlClient _client;


        public HandleDatabaseSearchBy([NotNull] IQuandlClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        public async Task<DatabaseSearch> Handle(DatabaseSearchBy query)
        {
            return await _client.GetAsync<DatabaseSearch>(query.ToQuandlClientRequestParameters());
        }
    }
}