using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Quandl;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Api.Transactions;
using NQuandl.Domain.Quandl.RequestParameters;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Domain.Quandl.Requests
{
    // https://www.quandl.com/api/v3/databases/:database_code/codes
    // https://www.quandl.com/api/v3/databases/YC/codes
    // Returns a .ZIP with a csv containing a list of dataset codes and descriptions
    public class RequestDatabaseDatasetListBy : BaseQuandlRequest<Task<DatabaseDatasetList>>
    {
        public RequestDatabaseDatasetListBy([NotNull] string databaseCode)
        {
            if (databaseCode == null) throw new ArgumentNullException(nameof(databaseCode));
            DatabaseCode = databaseCode;
        }

        public string DatabaseCode { get; }
      
        public override string ToUri()
        {
            var pathSegment = $"{ApiVersion}/databases/{DatabaseCode}/codes";
            return pathSegment;
        }
    }

    public class HandleDatabaseDatasetListBy : IHandleQuandlRequest<RequestDatabaseDatasetListBy, Task<DatabaseDatasetList>>
    {
        private readonly IQuandlClient _client;
        private readonly IMapCsvStream _mapper;


        public HandleDatabaseDatasetListBy(IQuandlClient client, IMapCsvStream mapper)
        {
            if (client == null) throw new ArgumentNullException(nameof(client));
            if (mapper == null) throw new ArgumentNullException(nameof(mapper));


            _client = client;
            _mapper = mapper;
        }

        //todo move zip reader to .services
        public async Task<DatabaseDatasetList> Handle(RequestDatabaseDatasetListBy query)
        {
            var quandlResponse = await _client.GetStreamAsync(query.ToUri());
            var zipArchive = new ZipArchive(quandlResponse.ContentStream);
            var csvFile = new StreamReader(zipArchive.Entries[0].Open());
            var datasets = await _mapper.MapToDataset(csvFile);
            var databaseDatasetList = new DatabaseDatasetList
            {
                QuandlClientResponseInfo = quandlResponse.QuandlClientResponseInfo,
                Datasets = datasets
            };
            
            return databaseDatasetList;
        }
    }
}