using System;
using System.Threading.Tasks;
using NQuandl.Api;
using NQuandl.Api.Helpers;
using NQuandl.Domain.Responses;

namespace NQuandl.Domain.Queries
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
            return await _queries.Execute(new QuandlQueryBy<DatabaseList>(query.ToQuandlClientRequestParameters()));
        }
    }
}