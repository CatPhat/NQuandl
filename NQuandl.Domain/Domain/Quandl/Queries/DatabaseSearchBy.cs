using System;
using System.Threading.Tasks;
using NQuandl.Api;
using NQuandl.Api.Helpers;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Domain.Quandl.Queries
{
    // https://www.quandl.com/api/v3/databases.json
    public class DatabaseSearchBy : IDefineQuery<Task<DatabaseSearch>>
    {
        public ResponseFormat ResponseFormat => ResponseFormat.JSON;

        // optional
        public string Query { get; set; }
        public int? PerPage { get; set; }
        public int? Page { get; set; }
        public string ApiVersion => RequestParameterConstants.ApiVersion;

       
    }

    public class HandleDatabaseSearchBy : IHandleQuery<DatabaseSearchBy, Task<DatabaseSearch>>
    {
        private readonly IProcessQueries _queries;

        public HandleDatabaseSearchBy(IProcessQueries queries)
        {
            if (queries == null) throw new ArgumentNullException(nameof(queries));

            _queries = queries;
        }

        public async Task<DatabaseSearch> Handle(DatabaseSearchBy query)
        {
            return await _queries.Execute(new QuandlQueryBy<DatabaseSearch>(query.ToQuandlClientRequestParameters()));
        }
    }
}