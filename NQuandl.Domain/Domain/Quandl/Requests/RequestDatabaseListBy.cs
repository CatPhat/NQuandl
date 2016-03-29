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
    /// Description: You can download a list of all databases on Quandl, along with their respective metadata, by making this call. 
    /// Databases are returned 100 results at a time.
    /// URL Template: https://www.quandl.com/api/v3/databases
    /// URL Example: https://www.quandl.com/api/v3/databases.json?per_page=3
    /// </summary>
    public class RequestDatabaseListBy : BaseQuandlRequest<Task<JsonResultDatabaseList>>
    {
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

    public class HandleRequestDatabaseListBy : IHandleQuandlRequest<RequestDatabaseListBy, Task<JsonResultDatabaseList>>
    {
        private readonly IQuandlClient _client;


        public HandleRequestDatabaseListBy([NotNull] IQuandlClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        public async Task<JsonResultDatabaseList> Handle(RequestDatabaseListBy query)
        {
            return await _client.GetAsync<JsonResultDatabaseList>(query.ToUri());
        }
    }
}