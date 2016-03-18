using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api;
using NQuandl.Api.Quandl;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Api.Transactions;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Domain.Quandl.Queries
{
    // https://www.quandl.com/api/v3/databases.json
    public class DatabaseListBy : IDefineQuery<Task<DatabaseList>>
    {
        public int? PerPage { get; set; }
        public int? Page { get; set; }
        public ResponseFormat ResponseFormat => ResponseFormat.JSON;

        public string ApiVersion => RequestParameterConstants.ApiVersion;
    }

    public class HandleDatabaseListBy : IHandleQuery<DatabaseListBy, Task<DatabaseList>>
    {
        private readonly IQuandlClient _client;


        public HandleDatabaseListBy([NotNull] IQuandlClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        public async Task<DatabaseList> Handle(DatabaseListBy query)
        {
            return await _client.GetAsync<DatabaseList>(query.ToQuandlClientRequestParameters());
        }
    }
}