using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using NQuandl.Api;
using NQuandl.Api.Quandl;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Api.Transactions;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Domain.Quandl.Queries
{
    // https://www.quandl.com/api/v3/databases/:database_code/codes
    // https://www.quandl.com/api/v3/databases/YC/codes
    // Returns a .ZIP with a csv containing a list of dataset codes and descriptions
    public class DatabaseDatasetListBy : IDefineQuery<Task<DatabaseDatasetList>>
    {
        public DatabaseDatasetListBy(string databaseCode)
        {
            DatabaseCode = databaseCode;
        }

        public string DatabaseCode { get; }
        public string ApiVersion => RequestParameterConstants.ApiVersion;
    }

    public class HandleDatabaseDatasetListBy : IHandleQuery<DatabaseDatasetListBy, Task<DatabaseDatasetList>>
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
        public async Task<DatabaseDatasetList> Handle(DatabaseDatasetListBy query)
        {
            var quandlResponse = await _client.GetStreamAsync(query.ToQuandlClientRequestParameters());
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