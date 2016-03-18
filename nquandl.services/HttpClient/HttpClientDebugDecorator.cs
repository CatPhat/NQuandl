using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NQuandl.Api.Quandl;
using NQuandl.Api.Quandl.Helpers;
using NQuandl.Domain.Quandl.RequestParameters;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Services.HttpClient
{
    public class HttpClientDebugDecorator : IHttpClient
    {
        private readonly Func<IHttpClient> _httpFactory;
        public HttpClientDebugDecorator([NotNull] Func<IHttpClient> httpFactory)
        {
            if (httpFactory == null) throw new ArgumentNullException(nameof(httpFactory));
            _httpFactory = httpFactory;
        }

     

        public Task<HttpClientResponse> GetAsync(string requestUri)
        {
          
            Console.WriteLine(requestUri);
            switch (requestUri)
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

        private Task<HttpClientResponse> GetDatasetByFredGdp()
        {
            return
                GetStreamFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatasetFredGdp.json");
        }

        private Task<HttpClientResponse> GetDatabaseMetadataByYC()
        {
            return
                GetStreamFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatabaseMetadataYC.json");
        }

        private Task<HttpClientResponse> GetDatabaseListBy()
        {
            return
                GetStreamFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatabaseList.json");
        }

        private Task<HttpClientResponse> GetDatabaseSearchByStockPrice()
        {
            return
                GetStreamFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\DatabaseSearchStockPrice.json");
        }

        private Task<HttpClientResponse> GetDatabaseDatasetListByYC()
        {
            return
                GetStreamFromFile(
                    @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\YC-datasets-codes.zip");
        }

        private Task<HttpClientResponse> GetStreamFromFile(string filePath)
        {
            var response = new HttpClientResponse();

            var stream = File.OpenRead(filePath);

            response.ContentStream = stream;
            return Task.FromResult(response);
        }
    }
}
