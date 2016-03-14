using NQuandl.Api;
using SimpleInjector;

namespace NQuandl.Services.HttpClient
{
    public static class CompositionRoot
    {
        public static void RegisterHttpClient(this Container container)
        {
            container.RegisterSingleton<IHttpClient, HttpClient>();
        }
    }
}