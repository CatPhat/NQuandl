using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using NQuandl.Client;
using NQuandl.Queue;

namespace NQuandl.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var results = new GetResults();

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            Task.WaitAll(results.GetAllResult());
            stopwatch.Stop();

            Console.WriteLine("done | time: " + stopwatch.Elapsed);
            Console.ReadLine();
        }
    }


    public class GetResults
    {
        public async Task<int> GetAllResult()
        {
            var task = Task.WhenAll(Get1(), Get2());
            int? counter = null;
            PrintStatus();
            while (!task.IsCompleted)
            {
                if (counter.HasValue && counter.Value != NQueue.GetQueueStatus().RequestsRemaining)
                {
                    PrintStatus();
                }
                counter = NQueue.GetQueueStatus().RequestsRemaining;
            }
            return await Task.FromResult(0);
        }

        public void PrintStatus()
        {
            Console.WriteLine("Total: {0} | Processed: {1} | Remaining {2}", NQueue.GetQueueStatus().TotalRequests, NQueue.GetQueueStatus().RequestsProcessed, NQueue.GetQueueStatus().RequestsRemaining);

        }

        public async Task<int> Get1()
        {
            var requests = new List<TestRequest>();
            for (var i = 1; i <= 20; i++)
            {
                requests.Add(new TestRequest());
            }

            var actionDelegate = new Actions();

            var responses = await NQueue.SendRequests(requests);
          //  actionDelegate.ActionDelegate(responses);
            return await Task.FromResult(0);
        }

        public async Task<int> Get2()
        {
            var requests = new List<TestRequest2>();
            for (var i = 1; i <= 20; i++)
            {
                requests.Add(new TestRequest2());
            }

            var actionDelegate = new Actions();

            var responses = await NQueue.SendRequests(requests);
        //    actionDelegate.ActionDelegate(responses);
            return await Task.FromResult(0);
        }
    }


    public class Actions
    {
        public void ActionDelegate(IEnumerable<TestResponse> responses)
        {
            foreach (var response in responses)
            {
                Console.Write("Request[1] Count:" + response.RequestCount + " Request[1]: " +
                      response.RequestType + " | Second: " + response.Second + " | " +
                      response.UniqueId + " | " + response.Milliseconds);
                Console.WriteLine();
            }


        }

        public void ActionDelegate(IEnumerable<TestResponse2> responses)
        {
            foreach (var response in responses)
            {
                Console.Write("Request[2] Count:" + response.RequestCount + " Request[2]: " +
                        response.RequestType + " | Second: " + response.Second + " | " +
                        response.UniqueId2 + " | " + response.Milliseconds);
                Console.WriteLine();
            }



        }

        public void QueueDelegate(QueueStatus queueStatus)
        {
            Console.WriteLine("Requests Remaining: " + queueStatus.RequestsRemaining + " Requests Processed: " + queueStatus.RequestsProcessed);
        }
    }

}

