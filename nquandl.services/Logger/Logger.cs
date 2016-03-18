using System;
using System.IO;
using System.Threading;

namespace NQuandl.Services.Logger
{
    public class Logger : ILogger
    {

        private static int InboundRequests;
        private static readonly int CompletedRequests;

        private int RequestsRemaining => InboundRequests - CompletedRequests;


        public void AddInboundRequest()
        {
            InboundRequests = InboundRequests + 1;
        }


        public void Write(string logMessage)
        {
            Log(logMessage);
        }

        private void Log(string logMessage)
        {
          
                DateTime now = DateTime.Now;
                Console.WriteLine("{0} {1} {2}", now.ToLongTimeString(), now.ToLongDateString(), logMessage);
        }

       
    }
}