using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Queries
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
        private readonly IProcessQueries _queries;

        public HandleDatabaseDatasetListBy(IProcessQueries queries, IQuandlClient client)
        {
            if (queries == null) throw new ArgumentNullException(nameof(queries));
            if (client == null) throw new ArgumentNullException(nameof(client));

            _queries = queries;
            _client = client;
        }

        public async Task<DatabaseDatasetList> Handle(DatabaseDatasetListBy query)
        {
            var fullResponse = await _client.GetFullResponseAsync(query.ToQuandlClientRequestParameters());


            var zipStream = await fullResponse.Content.ReadAsStreamAsync();
            var zipArchive = new ZipArchive(zipStream);

            var csvFile = new StreamReader(zipArchive.Entries[0].Open());
            var csv = new CsvReader(csvFile);

            csv.Configuration.HasHeaderRecord = false;
            csv.Configuration.SkipEmptyRecords = true;
            csv.Configuration.ThrowOnBadData = true;
            csv.Configuration.IgnoreHeaderWhiteSpace = true;
            csv.Configuration.RegisterClassMap<DatabaseDatasetCsvRowMapper>();

            var csvRecords = csv.GetRecords<DatabaseDatasetCsvRow>().ToList();
            var databaseDatasetList = new DatabaseDatasetList
            {
                HttpResponseMessage = fullResponse,
                Datasets = csvRecords
            };

            return databaseDatasetList;
        }
    }
}