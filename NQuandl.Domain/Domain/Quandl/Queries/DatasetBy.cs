﻿using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Quandl;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Api.Transactions;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Domain.Quandl.Queries
{
    // https://www.quandl.com/api/v3/datasets/WIKI/FB.json
    public class DatasetBy : IDefineQuery<Task<DatabaseDataset>>

    {
        public DatasetBy(string databaseCode, string datasetCode)
        {
            DatabaseCode = databaseCode;
            DatasetCode = datasetCode;
        }

        public ResponseFormat ResponseFormat => ResponseFormat.JSON;

        public int? Limit { get; set; }
        public int? Rows { get; set; }
        public int? ColumnIndex { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Order? Order { get; set; }
        public Collapse? Collapse { get; set; }
        public Transform? Transform { get; set; }

        public string DatabaseCode { get; private set; }
        public string DatasetCode { get; private set; }
        public string ApiVersion => RequestParameterConstants.ApiVersion;
    }

    public class HandleDatasetBy : IHandleQuery<DatasetBy, Task<DatabaseDataset>>

    {
        private readonly IQuandlClient _client;


        public HandleDatasetBy([NotNull] IQuandlClient client)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            _client = client;
        }

        public async Task<DatabaseDataset> Handle(DatasetBy query)
        {
            return await _client.GetAsync<DatabaseDataset>(query.ToQuandlClientRequestParameters());
        }
    }
}