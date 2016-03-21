using System;
using System.IO;
using System.Threading;

namespace NQuandl.Services.Logger
{
    public class Logger : ILogger
    {

        private static int InboundRequests;
        private static int CompletedRequests;

     


        public void AddInboundRequest(string request)
        {
            InboundRequests = InboundRequests + 1;
            Log("+ 1 Inbound | " + request);
            Log("Inbound Requests: " + InboundRequests);
        }

        public void AddCompletedRequest(string request)
        {
            CompletedRequests = CompletedRequests + 1;
            InboundRequests = InboundRequests - 1;

            Log("---------------");
            Log("+1 Completed | " + request);
            Log("Time Left: " + TimeSpan.FromMilliseconds(300 * InboundRequests));
            Log("Completed Requests: " + CompletedRequests);
            Log("Inbound Requests: " + InboundRequests);
            

        }


        public void Write(string logMessage)
        {
            Log(logMessage);
        }

        private void Log(string logMessage)
        {
          
                DateTime now = DateTime.Now;
                NonBlockingConsole.WriteLine(String.Format("{0} {1} {2}", now.ToLongTimeString(), now.ToLongDateString(), logMessage));
        }

       
    }
}