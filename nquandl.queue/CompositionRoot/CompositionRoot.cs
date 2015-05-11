﻿using System;
using NQuandl.Client;
using NQuandl.Client.Interfaces;
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

            container.RegisterSingle<IJsonServiceQueue, JsonServiceQueue>();
            container.RegisterSingleDecorator(typeof(IJsonServiceQueue), typeof(JsonServiceQueueDecorator));
            
        }
    }
}
