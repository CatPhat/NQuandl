using NQuandl.Api;
using NQuandl.Api.Quandl;
using SimpleInjector;

namespace NQuandl.Services.Quandl
{
    public static class CompositionRoot
    {
        public static void RegisterQuandlClient(this Container container)
        {
            container.Register<IQuandlClient, QuandlClient>();
           
            container.RegisterDecorator<IQuandlClient, QuandlClientDebugDecorator>();
            container.RegisterDecorator<IQuandlClient, QuandlClientRateLimiterDecorator>();

        }
    }
}