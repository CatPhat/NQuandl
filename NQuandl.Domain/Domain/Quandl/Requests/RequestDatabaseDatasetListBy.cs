using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Quandl;
using NQuandl.Api.Transactions;
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

    public class HandleDatabaseDatasetListBy :
        IHandleQuandlRequest<RequestDatabaseDatasetListBy, Task<CsvResultDatabaseDatasetList>>
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
        public async Task<CsvResultDatabaseDatasetList> Handle(RequestDatabaseDatasetListBy query)
        {
            var quandlResponse = await _client.GetStreamAsync(query.ToUri());
            var zipArchive = new ZipArchive(quandlResponse.ContentStream);
            var csvFile = new StreamReader(zipArchive.Entries[0].Open());
            var datasets = await _mapper.MapToDataset(csvFile);
            var databaseDatasetList = new CsvResultDatabaseDatasetList
            {
                QuandlClientResponseInfo = quandlResponse.QuandlClientResponseInfo,
                Datasets = datasets
            };

            return databaseDatasetList;
        }
    }
}