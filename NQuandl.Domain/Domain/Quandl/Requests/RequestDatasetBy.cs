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
    // https://www.quandl.com/api/v3/datasets/WIKI/FB.json
    public class RequestDatasetBy : BaseQuandlRequest<Task<DatabaseDataset>>

    {
        public RequestDatasetBy([NotNull] string databaseCode, [NotNull] string datasetCode)
        {
            if (databaseCode == null) throw new ArgumentNullException(nameof(databaseCode));
            if (datasetCode == null) throw new ArgumentNullException(nameof(datasetCode));
            DatabaseCode = databaseCode;
            DatasetCode = datasetCode;
        }

        public int? Limit { get; set; }
        public int? Rows { get; set; }
        public int? ColumnIndex { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Order? Order { get; set; }
        public Collapse? Collapse { get; set; }
        public Transform? Transform { get; set; }

        public string DatabaseCode { get; }
        public string DatasetCode { get; }

        public override string ToUri()
        {
            return new QuandlClientRequestParameters
            {
                PathSegment = $"{ApiVersion}/datasets/{DatabaseCode}/{DatasetCode}.{ResponseFormat.GetStringValue()}",
                QueryParameters = this.ToRequestParameterDictionary()
            }.ToUri();
        }
    }

    public class HandleRequestDatasetBy : IHandleQuandlRequest<RequestDatasetBy, Task<DatabaseDataset>>

    {
        private readonly IQuandlClient _client;
        public HandleRequestDatasetBy([NotNull] IQuandlClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        public async Task<DatabaseDataset> Handle(RequestDatasetBy query)
        {
            return await _client.GetAsync<DatabaseDataset>(query.ToUri());
        }
    }
}