using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reactive.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Client.Api.Quandl;
using NQuandl.Client.Api.Quandl.Helpers;
using NQuandl.Client.Api.Transactions;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Domain.Requests
{
    /// <summary>
    /// Description: You can download a list of all dataset codes in a database in a single call, by appending /codes to your database request.
    /// URL Template: https://www.quandl.com/api/v3/databases/:database_code/codes
    /// URL Example: https://www.quandl.com/api/v3/databases/YC/codes
    /// </summary>
    public class RequestDatabaseDatasetListBy : BaseQuandlRequest<Task<CsvResultDatabaseDatasetList>>
    {
        /// <summary> 
        /// </summary>
        /// <param name="databaseCode"> 
        /// The unique database code on Quandl (ex. YC)
        /// </param>
        public RequestDatabaseDatasetListBy([NotNull] string databaseCode)
        {
            if (databaseCode == null)
                throw new ArgumentNullException(nameof(databaseCode));
            DatabaseCode = databaseCode;
        }

        public string DatabaseCode { get; }

        public override string ToUri()
        {
            return new QuandlClientRequestParameters
            {
                PathSegment = $"{ApiVersion}/databases/{DatabaseCode}/codes",
                QueryParameters = new Dictionary<string, string>()
            }.ToUri();
        }
    }

    public class HandleDatabaseDatasetListBy :
        IHandleQuandlRequest<RequestDatabaseDatasetListBy, Task<CsvResultDatabaseDatasetList>>
    {
        private readonly IQuandlClient _client;


        public HandleDatabaseDatasetListBy(IQuandlClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            _client = client;
        }

        //todo move zip reader to .services
        //todo needs to yield per row from zip file due to memory constraints
        public async Task<CsvResultDatabaseDatasetList> Handle(RequestDatabaseDatasetListBy query)
        {
            var quandlResponse = await _client.GetStreamAsync(query.ToUri());


            var zipArchive = new ZipArchive(quandlResponse.ContentStream, ZipArchiveMode.Read);

            var csvFile = new StreamReader(zipArchive.Entries[0].Open());

            var datasets = await GetCsvDatabaseDatasetsAsync(csvFile).ToList();

            var databaseDatasetList = new CsvResultDatabaseDatasetList
            {
                QuandlClientResponseInfo = quandlResponse.QuandlClientResponseInfo,
                Datasets = datasets
            };

            return databaseDatasetList;
        }

        private IObservable<CsvDatabaseDataset> GetCsvDatabaseDatasetsAsync(StreamReader csvFile)
        {
            return Observable.Create<CsvDatabaseDataset>(async obs =>
            {
                using (csvFile)
                {
                    string line;
                    while ((line = await csvFile.ReadLineAsync()) != null)
                    {
                        var csvDatabaseDataset = ParseCsvDatabaseDataset(line);

                        obs.OnNext(csvDatabaseDataset);
                    }
                    obs.OnCompleted();
                }
            });
        }

        private static CsvDatabaseDataset ParseCsvDatabaseDataset(string csvLine)
        {
            var commaIndex = csvLine.IndexOf(",", StringComparison.Ordinal);
            var quandlCode = csvLine.Substring(0, commaIndex);
            var splitQuandlCode = quandlCode.Split('/');
            var description = csvLine.Substring(commaIndex + 1);

            return new CsvDatabaseDataset
            {
                QuandlCode = quandlCode,
                DatasetDescription = description,
                DatabaseCode = splitQuandlCode[0],
                DatasetCode = splitQuandlCode[1]
            };
        }
    }
}