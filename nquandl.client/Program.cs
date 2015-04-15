using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Client;
using NQuandl.Client.Helpers;
using NQuandl.Queue;




namespace NQuandl.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var results = new GetResults();

            Task.WaitAll(results.GetAllResult());

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }


    public class GetResults
    {
        public async Task<int> GetAllResult()
        {
            Task.WaitAll(Get1(), Get2());
            return await Task.FromResult(0);
        }

        public async Task<int> Get1()
        {
            var requests = new List<TestRequest>();
            for (var i = 1; i <= 2; i++)
            {
                requests.Add(new TestRequest());
            }

            var actionDelegate = new Actions();

            var responses = await NQueue.SendRequests(requests, actionDelegate.QueueDelegate);
            foreach (var testResponse in responses)
            {
                actionDelegate.ActionDelegate(testResponse.QuandlResponse);
            }
            return await Task.FromResult(0);
        }

        public async Task<int> Get2()
        {
            var requests = new List<TestRequest2>();
            for (var i = 1; i <= 2; i++)
            {
                requests.Add(new TestRequest2());
            }

            var actionDelegate = new Actions();

            var responses = await NQueue.SendRequests(requests, actionDelegate.QueueDelegate);
            foreach (var testResponse in responses)
            {
                actionDelegate.ActionDelegate(testResponse.QuandlResponse);
            }
            return await Task.FromResult(0);
        }
    }


    public class Actions
    {
        public void ActionDelegate(TestResponse response)
        {
           
            Console.Write("Request[1] Count:" + response.RequestCount + " Request[1]: " +
                          response.RequestType + " | Second: " + response.Second + " | " +
                          response.UniqueId + " | " + response.Milliseconds);
            Console.WriteLine();
        }

        public void ActionDelegate(TestResponse2 quandlResponse)
        {
            Console.Write("Request[2] Count:" + quandlResponse.RequestCount + " Request[2]: " +
                          quandlResponse.RequestType + " | Second: " + quandlResponse.Second + " | " +
                          quandlResponse.UniqueId2 + " | " + quandlResponse.Milliseconds);
            Console.WriteLine();
        }

        public void QueueDelegate(QueueStatus queueStatus)
        {
            Console.WriteLine("Requests Remaining: " + queueStatus.RequestsRemaining + " Requests Processed: " + queueStatus.RequestsProcessed);
        }
    }

}

