using Microsoft.Extensions.Configuration;
using NQuandl.Client.Services.Configuration;

namespace NQuandl.Client.SimpleInjector.Extensions
{
    public static class HttpClientConfigurationExtensions
    {
        public static HttpClientConfiguration GetHttpClientConfiguration(this IConfiguration configuration,
            string section = "nquandl.client")
        {
            return configuration.GetConfiguration<HttpClientConfiguration>(section);
        }
    }
}