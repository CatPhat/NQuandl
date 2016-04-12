using System;
using JetBrains.Annotations;
using NQuandl.Client.Api.Quandl;
using NQuandl.Client.Services.Configuration;
using NQuandl.Client.Services.HttpClient;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Client.SimpleInjector.HttpClient
{
    public class Package : IPackage
    {
        private readonly IHttpClientConfiguration _configuration;

        public Package([NotNull] IHttpClientConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));
            _configuration = configuration;
        }

        public void RegisterServices(Container container)
        {
            container.RegisterSingleton<IHttpClient>(() => new Services.HttpClient.HttpClient(_configuration));
            //  container.RegisterSingleton<IHttpResponseCache, HttpResponseCache>();
            container.RegisterDecorator<IHttpClient, HttpClientDebugDecorator>(Lifestyle.Transient);
            // container.RegisterDecorator<IHttpClient, HttpClientRateLimiterDecorator>(Lifestyle.Transient);
            // container.RegisterDecorator<IHttpClient, HttpClientTaskQueueDecorator>(Lifestyle.Singleton);
            //  container.RegisterDecorator<IHttpClient, HttpClientLoggerDecorator>(Lifestyle.Transient);
        }
    }
}