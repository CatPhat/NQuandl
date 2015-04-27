using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Client;
using NQuandl.Client.Models.QuandlRequests;
using NQuandl.Queue;

namespace NQuandl.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var quandlCode = new QuandlCode { DatabaseCode = "WSJ", TableCode = "MILK" };
            var request1 = new RequestString(quandlCode,

                new OptionalRequestParameters
                {
                    SortOrder = SortOrder.Ascending,
                    Column = 3,
                    DateRange = new DateRange
                    {
                        TrimStart = DateTime.Now,
                        TrimEnd = DateTime.Today.AddDays(-20)
                    },
                    ExcludeData = Exclude.False,
                    ExcludeHeaders = Exclude.False,
                    Rows = 6,
                    Transformation = Transformation.CumulativeSum
                });


            var client = new QuandlService();
            var response = client.GetAsync(request1);
            Console.WriteLine(response.Result.String);

            Console.WriteLine("done | time: " + NQueue.GetQueueStatus().TimeElapsed);
            Console.ReadLine();
        }
    }


    public class QuandlRealDeal
    {
        public async Task<IEnumerable<RequestStringResponse>> GetStringResponse()
        {
            var quandlCode = new QuandlCode { DatabaseCode = "WSJ", TableCode = "MILK" };
            var request1 = new RequestString(quandlCode,

                new OptionalRequestParameters
                {
                    SortOrder = SortOrder.Ascending,
                    Column = 3,
                    DateRange = new DateRange
                    {
                        TrimStart = DateTime.Now,
                        TrimEnd = DateTime.Today.AddDays(-20)
                    },
                    ExcludeData = Exclude.False,
                    ExcludeHeaders = Exclude.False,
                    Rows = 6,
                    Transformation = Transformation.CumulativeSum
                });


            var request2 = new RequestString(quandlCode,

                new OptionalRequestParameters
                {
                    SortOrder = SortOrder.Ascending,
                    Column = 3,
                    DateRange = new DateRange
                    {
                        TrimStart = DateTime.Now,
                        TrimEnd = DateTime.Today.AddDays(-20)
                    },
                    ExcludeData = Exclude.False,
                    ExcludeHeaders = Exclude.False,
                    Rows = 6,
                    Transformation = Transformation.CumulativeSum
                });
            var requestList = new List<RequestString> { request1, request2 };
            var responses = await NQueue.SendRequests(requestList);
            return await Task.FromResult(responses);
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

