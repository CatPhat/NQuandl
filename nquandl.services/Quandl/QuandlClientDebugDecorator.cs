using System;
using System.IO;
using System.Threading.Tasks;
using NQuandl.Api.Quandl;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Domain.Quandl.RequestParameters;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Services.Quandl
{
    public class QuandlClientDebugDecorator : IQuandlClient
    {
        private readonly IQuandlClient _client;

        public QuandlClientDebugDecorator(IQuandlClient client)
        {
            _client = client;
        }

        public async Task<ResultStringWithQuandlResponseInfo> GetStringAsync(QuandlClientRequestParameters parameters)
        {
            var result = new ResultStringWithQuandlResponseInfo();
            var response = await GetAsync(parameters);

            using (var sr = new StreamReader(response.ContentStream))
            {
                result.ContentString = await sr.ReadToEndAsync();
                return result;
            }
        }

        public async Task<ResultStreamWithQuandlResponseInfo> GetStreamAsync(QuandlClientRequestParameters parameters)
        {
            return await GetAsync(parameters);
        }

        public async Task<TResult> GetAsync<TResult>(QuandlClientRequestParameters parameters)
            where TResult : ResultWithQuandlResponseInfo
        {
            var response = await GetAsync(parameters);
            var result = response.ContentStream.DeserializeToEntity<TResult>();
            result.QuandlClientResponseInfo = response.QuandlClientResponseInfo;

            return result;
        }

        public Task<ResultStreamWithQuandlResponseInfo> GetAsync(QuandlClientRequestParameters parameters)
        {
            var uri = parameters.ToUri();
            Console.WriteLine(uri);
             switch (uri)
            {
                case "api/v3/databases/YC/codes":
                    return GetDatabaseDatasetListByYC();

                case "api/v3/databases.json?query=stock%2Bprice":
                    return GetDatabaseSearchByStockPrice();

                case "api/v3/databases.json":
                    return GetDatabaseListBy();

                case "api/v3/databases/YC.json?database_code=YC":
                    return GetDatabaseMetadataByYC();

                case "api/v3/datasets/FRED/GDP.json":
                    return GetDatasetByFredGdp();
            }
            return null;
        }

        private Task<ResultStreamWithQuandlResponseInfo> GetDatasetByFredGdp()
        {
            return
                GetStreamFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatasetFredGdp.json");
        }

        private Task<ResultStreamWithQuandlResponseInfo> GetDatabaseMetadataByYC()
        {
            return
                GetStreamFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatabaseMetadataYC.json");
        }

        private Task<ResultStreamWithQuandlResponseInfo> GetDatabaseListBy()
        {
            return
                GetStreamFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatabaseList.json");
        }

        private Task<ResultStreamWithQuandlResponseInfo> GetDatabaseSearchByStockPrice()
        {
            return
                GetStreamFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatabaseSearchStockPrice.json");
        }

        private Task<ResultStreamWithQuandlResponseInfo> GetDatabaseDatasetListByYC()
        {
            return
                GetStreamFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\YC-datasets-codes.zip");
        }

        private Task<ResultStreamWithQuandlResponseInfo> GetStreamFromFile(string filePath)
        {
            var response = new ResultStreamWithQuandlResponseInfo();

            var stream = File.OpenRead(filePath);

            response.ContentStream = stream;
            return Task.FromResult(response);
        }
    }
}