using System;
using NQuandl.Services.Configuration;
using NQuandl.Services.HttpClient;
using NQuandl.Services.Quandl;
using NQuandl.Services.Quandl.Mapper;
using NQuandl.Services.Transactions;
using SimpleInjector;

namespace NQuandl.Services.CompositionRoot
{
    public static class CompositionRoot
    {
        public static void ComposeRoot(this Container container, RootCompositionSettings settings = null)
        {
            settings = settings ?? new RootCompositionSettings();

            container.Register<IServiceProvider>(() => container, Lifestyle.Singleton);
            container.RegisterConfiguration();
            container.RegisterQueryTransactions(settings.QueryHandlerAssemblies);
            container.RegisterHttpClient();
            container.RegisterQuandlJsonMapper(settings.QuandlJsonMapperAssemblies);
            container.RegisterQuandlCsvMapper(settings.QuandlCsvMapperAssemblies);
            container.RegisterQuandlClient();
        }
    }
}