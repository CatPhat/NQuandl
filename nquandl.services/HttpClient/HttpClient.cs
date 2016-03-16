using System;
using NQuandl.Api;
using NQuandl.Api.Configuration;
using NQuandl.Api.Quandl;

namespace NQuandl.Services.HttpClient
{
    public class HttpClient : System.Net.Http.HttpClient, IHttpClient
    {
        private readonly AppConfiguration _configuration;

        public HttpClient(AppConfiguration configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            _configuration = configuration;

            BaseAddress = new Uri(_configuration.BaseUrl);
        }
    }
}