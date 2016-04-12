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

    [UsedImplicitly]
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