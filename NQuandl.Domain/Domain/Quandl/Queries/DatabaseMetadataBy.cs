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
    // https://www.quandl.com/api/v3/databases/WIKI.json
    public class DatabaseMetadataBy : IDefineQuery<Task<DatabaseMetadata>>
    {
        public DatabaseMetadataBy(string databaseCode)
        {
            DatabaseCode = databaseCode;
        }

        public string DatabaseCode { get; }

        public ResponseFormat ResponseFormat => ResponseFormat.JSON;

        public string ApiVersion => RequestParameterConstants.ApiVersion;
    }

    public class HandleDatabaseMetadataBy : IHandleQuery<DatabaseMetadataBy, Task<DatabaseMetadata>>
    {
        private readonly IQuandlClient _client;


        public HandleDatabaseMetadataBy([NotNull] IQuandlClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        public async Task<DatabaseMetadata> Handle(DatabaseMetadataBy query)
        {
            return await _client.GetAsync<DatabaseMetadata>(query.ToQuandlClientRequestParameters()); ;
        }
    }
}