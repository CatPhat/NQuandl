using System;
using System.Collections.Concurrent;
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

           // var datasets = GetCsvRows(csvFile);
   
            var databaseDatasetList = new CsvResultDatabaseDatasetList
            {
                QuandlClientResponseInfo = quandlResponse.QuandlClientResponseInfo,
                Datasets = csvFile
            };

            return databaseDatasetList;
        }
       
      
    }
}