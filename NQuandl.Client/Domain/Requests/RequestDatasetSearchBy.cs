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
    /// Description: You can search for individual datasets on Quandl by making the following API request. 
    /// The API will return datasets related to your query, as well as datasets that belong to databases related to your query. 
    /// Datasets are returned 100 results at a time.
    /// URL Template: https://www.quandl.com/api/v3/datasets
    /// URL Example: https://www.quandl.com/api/v3/datasets.json?database_code=ODA&query=crude+oil&per_page=1&page=1
    /// </summary>
    public class RequestDatasetSearchBy : BaseQuandlRequest<Task<JsonResultDatasetSearch>>
    {
        /// <summary>
        /// Required: False.
        /// Description: You can restrict your search to a specific database by including a Quandl database code. 
        /// For example, the database code for “IMF Cross Country Macroeconomic Statistics” is ODA.
        /// </summary>
        public string DatabaseCode { get; set; }


        /// <summary>
        /// Required: False.
        /// Description: You can retrieve all datasets related to a search term using the query parameter. 
        /// Multiple search terms should be separated by a + character.
        /// </summary>
        public string Query { get; set; }


        /// <summary>
        /// Required: False.
        /// Description: The number of results per page that will be returned.
        /// </summary>
        public int? PerPage { get; set; }


        /// <summary>
        /// Required: False.
        /// Description: The current page of total available pages you wish to view.
        /// </summary>
        public int? Page { get; set; }


        public override string ToUri()
        {
            return new QuandlClientRequestParameters
            {
                PathSegment = $"{ApiVersion}/datasets.{ResponseFormat.GetStringValue()}",
                QueryParameters = this.ToRequestParameterDictionary()
            }.ToUri();
        }
    }

    [UsedImplicitly]
    public class HandleRequestDatasetSearchBy :
        IHandleQuandlRequest<RequestDatasetSearchBy, Task<JsonResultDatasetSearch>>
    {
        private readonly IQuandlClient _client;

        public HandleRequestDatasetSearchBy([NotNull] IQuandlClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        public async Task<JsonResultDatasetSearch> Handle(RequestDatasetSearchBy query)
        {
            return await _client.GetAsync<JsonResultDatasetSearch>(query.ToUri());
        }
    }
}