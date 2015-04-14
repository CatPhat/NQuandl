using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Client;
using NQuandl.Queue;




namespace NQuandl.TestConsole
{
    class Program
    {
        static void Main(string[] args)
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
            for (var i = 1; i <= 100; i++)
            {
                requests.Add(new TestRequest());
            }

            var actionDelegate = new Actions();

            var queueRequest = new QueueRequest<TestResponse>
            {
                ActionDelegate = actionDelegate.ActionDelegate,
                Requests = requests
            };

         
            await Task.Run(() => NQueue.SendRequests(queueRequest)).ConfigureAwait(false);
            return await Task.FromResult(0);
        }

        public async Task<int> Get2()
        {
            var requests = new List<TestRequest2>();
            for (var i = 1; i <= 100; i++)
            {
                requests.Add(new TestRequest2());
            }

            var actionDelegate = new Actions();

            var queueRequest = new QueueRequest<TestResponse2>
            {
                ActionDelegate = actionDelegate.ActionDelegate,
                Requests = requests
            };

         
            await Task.Run(() => NQueue.SendRequests(queueRequest)).ConfigureAwait(false);
            return await Task.FromResult(0);
        }
    }


    public class Actions
    {
        public void ActionDelegate(TestResponse quandlResponse)
        {
            Console.Write("Request[1] Count:" + quandlResponse.RequestCount + " Request[1]: " + quandlResponse.RequestType + " | Second: " + quandlResponse.Second + " | " + quandlResponse.UniqueId + " | " + quandlResponse.Milliseconds);
            Console.WriteLine();
        }

        public void ActionDelegate(TestResponse2 quandlResponse)
        {
            Console.Write("Request[2] Count:" + quandlResponse.RequestCount + " Request[2]: " + quandlResponse.RequestType + " | Second: " + quandlResponse.Second + " | " + quandlResponse.UniqueId2 + " | " + quandlResponse.Milliseconds);
            Console.WriteLine();
        }
    }
   



    
    }
}
