using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using NQuandl.Api;
using NQuandl.Api.Helpers;
using NQuandl.Domain.RequestParameters;
using NQuandl.Domain.Responses;

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

            }
            return null;

        }

        private Task<RawHttpContent> GetDatabaseDatasetListByYC()
        {
            var response = new RawHttpContent();
           
            var stream = File.OpenRead(
            @"C:\Users\USER9\Documents\GitHub\NQuandl\tests\NQuandl.Domain.Test\_etc\YC-datasets-codes.zip");

            response.Content = stream;
            response.IsStatusSuccessCode = true;
            response.StatusCode = "test";

         
        
            return Task.FromResult(response);
            
        }
    }

    
}
