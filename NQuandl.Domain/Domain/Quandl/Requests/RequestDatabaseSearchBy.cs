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
    // https://www.quandl.com/api/v3/databases.json
    public class RequestDatabaseSearchBy : BaseQuandlRequest<Task<JsonResultDatabaseSearch>>
    {
        // optional
        public string Query { get; set; }
        public int? PerPage { get; set; }
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