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
    /// Returns only dataset metadata and not associated dataset data.
    /// Example url: https://www.quandl.com/api/v3/datasets/:database_code/:dataset_code/metadata
    /// </summary>
    public class RequestDatasetMetadataBy : BaseQuandlRequest<Task<JsonResultDatasetMetadata>>,
        IDefineQuandlRequest<Task<JsonResultDatabaseMetadata>>
    {
        public RequestDatasetMetadataBy(string databaseCode, string datasetCode)
        {
            if (string.IsNullOrEmpty(databaseCode))
                throw new ArgumentException("Argument is null or empty", nameof(databaseCode));
            if (string.IsNullOrEmpty(datasetCode))
                throw new ArgumentException("Argument is null or empty", nameof(datasetCode));
            DatabaseCode = databaseCode;
            DatasetCode = datasetCode;
        }

        public string DatabaseCode { get; }
        public string DatasetCode { get; }

        public override string ToUri()
        {
            return new QuandlClientRequestParameters
            {
                PathSegment =
                    $"{ApiVersion}/datasets/{DatabaseCode}/{DatasetCode}/metadata.{ResponseFormat.GetStringValue()}",
                QueryParameters = this.ToRequestParameterDictionary()
            }.ToUri();
        }
    }

    [UsedImplicitly]
    public class HandleRequestDatasetMetadataBy :
        IHandleQuandlRequest<RequestDatasetMetadataBy, Task<JsonResultDatabaseMetadata>>
    {
        private readonly IQuandlClient _client;

        public HandleRequestDatasetMetadataBy([NotNull] IQuandlClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        public async Task<JsonResultDatabaseMetadata> Handle(RequestDatasetMetadataBy query)
        {
            return await _client.GetAsync<JsonResultDatabaseMetadata>(query.ToUri());
        }
    }
}