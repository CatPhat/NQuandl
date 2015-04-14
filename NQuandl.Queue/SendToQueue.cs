using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using NQuandl.Client;
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

        public static async Task SendRequests<T>(QueueRequest<T> queueRequest) where T : QuandlResponse
        {
            var createQueue = _container.GetInstance<IQuandlRequestQueue<T>>();
            await createQueue.ConsumeAsync(queueRequest);
        }
    }




   
    

   
}
