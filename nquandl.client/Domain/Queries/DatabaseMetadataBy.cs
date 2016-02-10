using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
{
    // https://www.quandl.com/api/v3/databases/WIKI.json
    public class DatabaseMetadataBy : IDefineQuery<Task<DatabaseMetadata>>
    {
        public DatabaseMetadataBy(string databaseCode)
        {
            DatabaseCode = databaseCode;
         
        }

        public string DatabaseCode { get; }
     
        public ResponseFormat ResponseFormat => ResponseFormat.JSON;

        public string ApiVersion => RequestParameterConstants.ApiVersion;
    }

    public class HandleDatabaseMetadataBy : IHandleQuery<DatabaseMetadataBy, Task<DatabaseMetadata>>
    {
        private readonly IProcessQueries _queries;

        public HandleDatabaseMetadataBy(IProcessQueries queries)
        {
            if (queries == null) throw new ArgumentNullException(nameof(queries));
            _queries = queries;
        }

        public async Task<DatabaseMetadata> Handle(DatabaseMetadataBy query)
        {
            var quandlClientRequestParameters = new QuandlClientRequestParameters
            {
                PathSegment = query.ToPathSegment(),
                QueryParameters = query.ToRequestParameterDictionary()
            };

            return await _queries.Execute(new QuandlQueryBy<DatabaseMetadata>(quandlClientRequestParameters));
        }
    }
}