using System;
using NQuandl.Client;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace NQuandl.Queue
{
    public static class CompositionRoot
    {
        public static void ComposeRoot(this Container container)
        {
            container.Register<IServiceProvider>(() => container, Lifestyle.Singleton);
            container.Register<IQuandlService>(() => new QuandlService());
            container.RegisterOpenGeneric(typeof(IQuandlRequestQueue<>), typeof(QuandlRequestQueue<>));
            container.RegisterSingle<IDownloadQueue,DownloadQueue>();
            container.RegisterSingle<IDownloadQueueLogger, DownloadQueueLogger>();
            container.RegisterSingleDecorator(typeof (IDownloadQueue), typeof (DownloadQueueDecorator));
            container.Register<IConsumeHttp>(() => new WebClientHttpConsumer());
            container.RegisterSingleDecorator(typeof(IConsumeHttp), typeof(WebClientHttpConsumerDecorator));
        }
    }
}
