using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Client.Api.Quandl;
using NQuandl.Client.Api.Quandl.Helpers;
using NQuandl.Client.Api.Transactions;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Requests
{
    /// <summary>
    /// Requests Quandl Dataset Data and associated Metadata
    /// Example URL: https://www.quandl.com/api/v3/datasets/:database_code/:dataset_code
    /// <returns>JsonResultDatasetDataAndMetadata</returns>
    /// </summary>
    public class RequestDatasetDataAndMetadataBy : BaseQuandlRequest<Task<JsonResultDatasetDataAndMetadata>>
    {
        /// <param name="databaseCode">
        /// Required: True.
        /// Description: Each database on Quandl has a unique database code. 
        /// For example, the database code for “Wiki EOD Stock Prices” has the Quandl code WIKI.
        /// </param>
        /// <param name="datasetCode">
        /// Required: True.
        /// Description: Each dataset on Quandl has a unique dataset code. 
        /// For example, to access the dataset named Apple Inc. (AAPL) use the dataset code AAPL. 
        /// The dataset code must be used in combination with the database code, for example, to retrieve the dataset named Apple, use WIKI/AAPL.
        /// </param>
        public RequestDatasetDataAndMetadataBy([NotNull] string databaseCode, [NotNull] string datasetCode)
        {
            if (databaseCode == null)
                throw new ArgumentNullException(nameof(databaseCode));
            if (datasetCode == null)
                throw new ArgumentNullException(nameof(datasetCode));
            DatabaseCode = databaseCode;
            DatasetCode = datasetCode;
        }

        /// <summary>
        /// Required: False.
        /// Description: You can use limit=n to get only the first n rows of your dataset. 
        /// Use limit=1 to get the latest observation for any dataset. 
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Required: False.
        /// Description: You can use rows=n to get only the first n rows of your dataset. 
        /// Use rows=1 to get the latest observation for any dataset.
        /// </summary>
        public int? Rows { get; set; }

        /// <summary>
        /// Required: False.
        /// Description: Request a specific column by passing the column_index=n parameter. 
        /// Column 0 is the date column and is always returned. Data begins at column 1.
        /// </summary>
        public int? ColumnIndex { get; set; }

        /// <summary>
        /// Required: False.
        /// Description: Retrieve data within a specific date range, by setting start dates for your query. 
        /// Set the start date with: start_date=yyyy-mm-dd
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Required: False
        /// Description: Retrieve data within a specific date range, by setting end dates for your query. 
        /// Set the end date with: end_date=yyyy-mm-dd
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Required: False.
        /// Description: Select the sort order by passing the parameter order=asc|desc.
        /// (Notice that the | in the parameter specification separates various mutually-exclusive options). 
        /// The default sort order is descending.
        /// </summary>
        public Order? Order { get; set; }

        /// <summary>
        /// Required: False.
        /// Description: Parameters to indicate the desired frequency. 
        /// When you change the frequency of a dataset, Quandl returns the last observation for the given period. 
        /// By collapsing a daily dataset to monthly, you will get a sample of the original dataset where the observation for each month is the last data point available for that month. 
        /// Set collapse with: collapse=none|daily|weekly|monthly|quarterly|annual
        /// </summary>
        public Collapse? Collapse { get; set; }

        /// <summary>
        /// Required: False
        /// Description: Perform calculations on your data prior to downloading. 
        /// The transformations currently availabe are row-on-row change, percentage change, cumulative sum, and normalize (set starting value at 100). 
        /// Set the transform parameter with: transform=none|diff|rdiff|cumul|normalize
        /// </summary>
        public Transform? Transform { get; set; }

        public string DatabaseCode { get; }
        public string DatasetCode { get; }

        public override string ToUri()
        {
            return new QuandlClientRequestParameters
            {
                PathSegment = $"{ApiVersion}/datasets/{DatabaseCode}/{DatasetCode}.{ResponseFormat.GetStringValue()}",
                QueryParameters = this.ToRequestParameterDictionary()
            }.ToUri();
        }
    }

    [UsedImplicitly]
    public class HandleRequestDatasetDataAndMetadataBy :
        IHandleQuandlRequest<RequestDatasetDataAndMetadataBy, Task<JsonResultDatasetDataAndMetadata>>

    {
        private readonly IQuandlClient _client;

        public HandleRequestDatasetDataAndMetadataBy([NotNull] IQuandlClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        public async Task<JsonResultDatasetDataAndMetadata> Handle(RequestDatasetDataAndMetadataBy query)
        {
            return await _client.GetAsync<JsonResultDatasetDataAndMetadata>(query.ToUri());
        }
    }
}