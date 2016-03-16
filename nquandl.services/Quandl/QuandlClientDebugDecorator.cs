using System;
using System.IO;
using System.Threading.Tasks;
using NQuandl.Api;
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

        public Task<RawHttpContent> GetFullResponseAsync(QuandlClientRequestParameters parameters)
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

        private Task<RawHttpContent> GetDatasetByFredGdp()
        {
            return
                GetStreamFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatasetFredGdp.json");
        }

        private Task<RawHttpContent> GetDatabaseMetadataByYC()
        {
            return
                GetStreamFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatabaseMetadataYC.json");
        }

        private Task<RawHttpContent> GetDatabaseListBy()
        {
            return GetStreamFromFile(@"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatabaseList.json");
        }

        private Task<RawHttpContent> GetDatabaseSearchByStockPrice()
        {
           return
                GetStreamFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatabaseSearchStockPrice.json");
        }

        private Task<RawHttpContent> GetDatabaseDatasetListByYC()
        {
            return
                GetStreamFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\YC-datasets-codes.zip");
        }

        private Task<RawHttpContent> GetStreamFromFile(string filePath)
        {
            var response = new RawHttpContent();

            var stream = File.OpenRead(filePath);

            response.Content = stream;
            response.IsStatusSuccessCode = true;
            response.StatusCode = "test";
            return Task.FromResult(response);
        }
    }
}