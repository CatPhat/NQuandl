using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
{
    // https://www.quandl.com/api/v3/databases.json
    public class DatabaseListBy : IDefineQuery<Task<DatabaseList>>
    {
        public int? PerPage { get; set; }
        public int? Page { get; set; }
        public ResponseFormat ResponseFormat => ResponseFormat.JSON;

        public string ApiVersion => RequestParameterConstants.ApiVersion;
    }

    public class HandleDatabaseListBy : IHandleQuery<DatabaseListBy, Task<DatabaseList>>
    {
        private readonly IProcessQueries _queries;

        public HandleDatabaseListBy(IProcessQueries queries)
        {
            if (queries == null) throw new ArgumentNullException(nameof(queries));

            _queries = queries;
        }

        public async Task<DatabaseList> Handle(DatabaseListBy query)
        {
            var quandlClientRequestParameters = new QuandlRestClientRequestParameters
            {
                PathSegment = $"{query.ApiVersion}/databases.{query.ResponseFormat.GetStringValue()}",
                QueryParameters = query.ToRequestParameterDictionary()
            };

            return await _queries.Execute(new QuandlQueryBy<DatabaseList>(quandlClientRequestParameters));
        }
    }
}