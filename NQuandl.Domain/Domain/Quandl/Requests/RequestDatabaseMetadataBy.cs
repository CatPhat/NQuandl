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
    /// <summary>
    /// Description: This call returns descriptive metadata for the specified database.
    /// URL Template: https://www.quandl.com/api/v3/databases/:database_code
    /// URL Example: https://www.quandl.com/api/v3/databases/WIKI.json
    /// </summary>
    public class RequestDatabaseMetadataBy : BaseQuandlRequest<Task<JsonResultDatabaseMetadata>>
    {

        /// <summary> 
        /// </summary>
        /// <param name="databaseCode">
        /// Required: True.
        /// Description: The unique database code on Quandl (ex. WIKI).
        /// </param>
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
        IHandleQuandlRequest<RequestDatabaseMetadataBy, Task<JsonResultDatabaseMetadata>>
    {
        private readonly IQuandlClient _client;

        public HandleRequestDatabaseMetadataBy([NotNull] IQuandlClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        public async Task<JsonResultDatabaseMetadata> Handle(RequestDatabaseMetadataBy query)
        {
            return await _client.GetAsync<JsonResultDatabaseMetadata>(query.ToUri());
        }
    }
}