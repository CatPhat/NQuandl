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
            var results2 = new GetResults();
            var verbose = new Verbose();
            var stopwatch = new Stopwatch();

            var task = Task.WhenAll(results.GetAllResult(), results2.GetAllResult());

            var counter = NQueue.GetQueueStatus().RequestsRemaining;
            verbose.PrintStatus();
            while (!task.IsCompleted && counter >= 0)
            {

                var currentCount = NQueue.GetQueueStatus().RequestsRemaining;
                if (counter != currentCount)
                {
                    Console.Clear();
                    verbose.PrintStatus();

                    counter = currentCount;

                }


            }


            Console.WriteLine("done | time: " + NQueue.GetQueueStatus().TimeElapsed);
            Console.ReadLine();
        }
    }

    public class Verbose
    {
        public void PrintStatus()
        {


            Console.WriteLine("                     Total: {0}", NQueue.GetQueueStatus().TotalRequests);
            Console.WriteLine("                 Remaining: {0}", NQueue.GetQueueStatus().RequestsRemaining);
            Console.WriteLine("                 Processed: {0}", NQueue.GetQueueStatus().RequestsProcessed);
            Console.WriteLine("              Time Elapsed: {0}", NQueue.GetQueueStatus().TimeElapsed);
            Console.WriteLine("            Time Remaining: {0}", NQueue.GetQueueStatus().TimeRemaining);
            Console.WriteLine("            RequestsPerSec: {0} ", NQueue.GetQueueStatus().RequestsPerSecond);
            Console.WriteLine("10 Minutes at Current Rate: {0} ", NQueue.GetQueueStatus().HowManyRequestsIn10MinutesAtCurrentRate);
            Console.WriteLine("\nLast Response: {0} \n", NQueue.GetQueueStatus().LastResponse);
            //Console.WriteLine("{0}",new string('.', NQueue.GetQueueStatus().RequestsRemaining));

        }


    }



    public class GetResults
    {
        private const int RequestCount = 1000;

        public async Task<int> GetAllResult()
        {
            var task = Task.WhenAll(Get1(), Get2());
            await task;
            return await Task.FromResult(0);
        }


        public async Task<int> Get1()
        {
            var requests = new List<TestRequest>();
            for (var i = 1; i <= RequestCount; i++)
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
            for (var i = 1; i <= RequestCount; i++)
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

