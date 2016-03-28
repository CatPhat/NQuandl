using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Quandl;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Api.Transactions;
using NQuandl.Domain.Quandl.RequestParameters;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Domain.Quandl.Requests
{
    // https://www.quandl.com/api/v3/databases/WIKI.json
    public class RequestDatabaseMetadataBy : BaseQuandlRequest<Task<DatabaseMetadata>>
    {
        public RequestDatabaseMetadataBy([NotNull] string databaseCode)
        {
            if (databaseCode == null) throw new ArgumentNullException(nameof(databaseCode));
            DatabaseCode = databaseCode;
        }

        public string DatabaseCode { get; }


        public override string ToUri()
        {
            return new QuandlClientRequestParameters
            {
                PathSegment = $"{ApiVersion}/databases/{DatabaseCode}.{ResponseFormat.GetStringValue()}",
                QueryParameters = this.ToRequestParameterDictionary()
            }.ToUri();
        }
    }

    public class HandleRequestDatabaseMetadataBy :
        IHandleQuandlRequest<RequestDatabaseMetadataBy, Task<DatabaseMetadata>>
    {
        private readonly IQuandlClient _client;

        public HandleRequestDatabaseMetadataBy([NotNull] IQuandlClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        public async Task<DatabaseMetadata> Handle(RequestDatabaseMetadataBy query)
        {
            return await _client.GetAsync<DatabaseMetadata>(query.ToUri());
        }
    }
}