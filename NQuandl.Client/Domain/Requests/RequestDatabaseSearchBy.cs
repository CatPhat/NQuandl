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
    /// Description: You can search for specific databases on Quandl by making the following API request. 
    /// The API will return databases related to your query. 
    /// Databases are returned 100 results at a time.
    /// URL Template: https://www.quandl.com/api/v3/databases
    /// URL Example: https://www.quandl.com/api/v3/databases.json?query=stock+price&per_page=1&page=1
    /// </summary>
    public class RequestDatabaseSearchBy : BaseQuandlRequest<Task<JsonResultDatabaseSearch>>
    {
        /// <summary>
        /// Required: False.
        /// Description: You can retrieve all databases related to a search term using the query parameter. 
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
                PathSegment = $"{ApiVersion}/databases.{ResponseFormat.GetStringValue()}",
                QueryParameters = this.ToRequestParameterDictionary()
            }.ToUri();
        }
    }

    [UsedImplicitly]
    public class HandleRequestDatabaseSearchBy : IHandleQuandlRequest<RequestDatabaseSearchBy, Task<JsonResultDatabaseSearch>>
    {
        private readonly IQuandlClient _client;


        public HandleRequestDatabaseSearchBy([NotNull] IQuandlClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        public async Task<JsonResultDatabaseSearch> Handle(RequestDatabaseSearchBy query)
        {
            return await _client.GetAsync<JsonResultDatabaseSearch>(query.ToUri());
        }
    }
}