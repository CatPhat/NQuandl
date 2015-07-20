using System;
using System.Threading.Tasks;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.Client.Domain.Queries
{
    public class GetRequestsNeededToDownloadEntireDatasetV2By : IDefineQuery<Task<int>>
    {
        public GetRequestsNeededToDownloadEntireDatasetV2By(string sourceCode)
        {
            SourceCode = sourceCode;
        }

        public string SourceCode { get; private set; }
    }

    public class HandleRequestsNeededToDownloadEntireDatasetV2ByQuery :
        IHandleQuery<GetRequestsNeededToDownloadEntireDatasetV2By, Task<int>>
    {
        private readonly IProcessQueries _queries;

        public HandleRequestsNeededToDownloadEntireDatasetV2ByQuery(IProcessQueries queries)
        {
            if (queries == null) throw new ArgumentNullException("queries");
            _queries = queries;
        }

        public async Task<int> Handle(GetRequestsNeededToDownloadEntireDatasetV2By query)
        {
            var requestParameters = new RequestParametersV2
            {
                ApiKey = QuandlServiceConfiguration.ApiKey,
                Page = 1,
                PerPage = 1,
                Query = "*",
                SourceCode = query.SourceCode
            };
            var response = await _queries.Execute(new RequestJsonResponseV2(requestParameters));

            var requestsNeeded = Math.Ceiling(response.total_count/300.00);
            return (int) requestsNeeded;
        }
    }
}