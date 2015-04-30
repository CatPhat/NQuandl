﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NQuandl.Client;
using NQuandl.Client.Models;
using NQuandl.Queue.Annotations;
using SimpleInjector;

namespace NQuandl.Queue
{
    public static class NQueue
    {
        private static readonly Container _container;

        static NQueue()
        {
            _container = new Container();
            _container.ComposeRoot();
            _container.Verify();
        }

        public static async Task<IEnumerable<T>> SendRequests<T>(IEnumerable<IQuandlRequest<T>> queueRequest) where T : QuandlResponse
        {
            var createQueue = _container.GetInstance<IQuandlRequestQueue<T>>();
            return await createQueue.ConsumeAsync(queueRequest);
        }

        public static async Task<IEnumerable<T>> SendRequests<T>(IEnumerable<IQuandlRequest<T>> queueRequest, QueueStatusDelegate queueStatusDelegate) where T : QuandlResponse
        {
            var createQueue = _container.GetInstance<IQuandlRequestQueue<T>>();
            return await createQueue.ConsumeAsync(queueRequest, queueStatusDelegate);
        }

        public static QueueStatus GetQueueStatus()
        {
            var queueLogger = _container.GetInstance<IDownloadQueueLogger>();
            return queueLogger.GetQueueStatus();
        }
    }




   
    

   
}
