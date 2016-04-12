using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.Client.Services.HttpClient
{
    public interface IHttpResponseCache
    {
        Task<HttpClientResponse> GetDatabaseMetadataByYc();
    }

    public class HttpResponseCache : IHttpResponseCache
    {
    


        private readonly byte[] _datasetByFredGdpBytes;
        private readonly byte[] _databaseMetadataByYcBytes;
        private readonly byte[] _databaseListBytes;
        private readonly byte[] _databaseSearchByStockPrice;
        private readonly byte[] _databaseDatasetListByYcBytes;
        private readonly SemaphoreSlim _semaphore;

        public HttpResponseCache()
        {

            _datasetByFredGdpBytes =
                GetBytesFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatasetFredGdp.json");

            _databaseMetadataByYcBytes =
                GetBytesFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatabaseMetadataYC.json");

            _databaseListBytes =
                GetBytesFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatabaseList.json");


            _databaseSearchByStockPrice =
                GetBytesFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatabaseSearchStockPrice.json");

            _databaseDatasetListByYcBytes =
                GetBytesFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\YC-datasets-codes.zip");

            _semaphore = new SemaphoreSlim(1);

        }


        public async Task<HttpClientResponse> GetDatabaseMetadataByYc()
        {
           // await _semaphore.WaitAsync();

            try
            {


               // await Task.Delay(100);
                var bytes = GetBytesFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatabaseMetadataYC.json");
                var stream = GetMemoryStreamFromBytes(bytes);

                var response = new HttpClientResponse
                {
                    IsStatusSuccessCode = true,
                    StatusCode = "DEBUG",
                    ResponseHeaders = new Dictionary<string, IEnumerable<string>>(),
                    ContentStream = await stream
                };

                return await Task.FromResult(response);
            }
            finally
            {

              //  _semaphore.Release();
            }
          
        }

      

        private Task<MemoryStream> GetMemoryStreamFromBytes(byte[] bytes)
        {
            var memoryStream = new MemoryStream();
            memoryStream.WriteAsync(bytes, 0, bytes.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return Task.FromResult(memoryStream);
        }

        private static byte[] GetBytesFromFile(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
    }
}
