using System;
using NQuandl.Services.Configuration;
using NQuandl.Services.HttpClient;
using NQuandl.Services.Logger;
using NQuandl.Services.PostgresEF7.CompositionRoot;
using NQuandl.Services.Quandl;
using NQuandl.Services.Quandl.Transactions;
using NQuandl.Services.RateGate;
using NQuandl.Services.TaskQueue;
using SimpleInjector;

namespace NQuandl.Services.CompositionRoot
{
    public static class CompositionRoot
    {
        public static void ComposeRoot(this Container container, RootCompositionSettings settings = null)
        {
            settings = settings ?? new RootCompositionSettings();


            container.Register<IServiceProvider>(() => container, Lifestyle.Singleton);
            container.RegisterConfiguration(settings.Configuration);
            container.RegisterQuandlRequestTransactions(settings.QuandlRequestHandlerAssemblies);
            container.RegisterHttpClient();

            container.RegisterQuandlClient();
            container.RegisterRateGate();
            container.RegisterLogger();
            container.RegisterTaskQueue();


            container.RegisterEntityFramework();
        }
    }
}