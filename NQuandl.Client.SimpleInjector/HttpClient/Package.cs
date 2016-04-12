using NQuandl.Client.Api.Quandl;
using NQuandl.Client.Services.HttpClient;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace NQuandl.Client.SimpleInjector.HttpClient
{
    public class Package : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.RegisterSingleton<IHttpClient, Services.HttpClient.HttpClient>();
            //  container.RegisterSingleton<IHttpResponseCache, HttpResponseCache>();
            container.RegisterDecorator<IHttpClient, HttpClientDebugDecorator>(Lifestyle.Transient);
            // container.RegisterDecorator<IHttpClient, HttpClientRateLimiterDecorator>(Lifestyle.Transient);
            // container.RegisterDecorator<IHttpClient, HttpClientTaskQueueDecorator>(Lifestyle.Singleton);
            //  container.RegisterDecorator<IHttpClient, HttpClientLoggerDecorator>(Lifestyle.Transient);
        }
    }
}