using System;
using NQuandl.Client;
using NQuandl.Client._OLD;
using NQuandl.Client._OLD.Interfaces.old;
using SimpleInjector;
using SimpleInjector.Extensions;

namespace NQuandl.Queue
{
    public static class CompositionRoot
    {

        public static void ComposeRoot(this Container container)
        {
            container.Register<IServiceProvider>(() => container, Lifestyle.Singleton);
            container.Register<IQuandlService>(() => new QuandlService(QuandlServiceConfiguration.BaseUrl));
           
         
           
            container.RegisterSingle<IQueueStatusLogger, QueueStatusLogger>();

            container.RegisterSingle<INQuandlQueue, NQuandlQueue>();
            container.RegisterSingleDecorator(typeof(INQuandlQueue), typeof(QuandlQueueDecorator));
            
        }
    }
}
