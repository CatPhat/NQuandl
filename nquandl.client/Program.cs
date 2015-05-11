using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Client;
using NQuandl.Client.Entities;
using NQuandl.Client.Helpers;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;
using NQuandl.Client.URIs;
using NQuandl.Queue;

namespace NQuandl.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var test1 = new QuandlQueueTest();
            var test2 = new QuandlQueueTest();
            var test3 = new QuandlQueueTest();
            var test4 = new QuandlQueueTest();
            var test5 = new QuandlQueueTest();
            var test6 = new QuandlQueueTest();
            var printStatus = new PrintStatus();
            var task = Task.WhenAll(test1.GetTestString(), test2.GetTest2String());
            while (task.IsCompleted == false)
            {
                printStatus.Print();
            }
         
              
            
           
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }


    public class TestRequest1 : IQuandlRequest
    {
        public IContainUri Uri
        {
            get { return new TestRequest1Uri(); }
        }
    }

    public class TestRequest1Uri : IContainUri
    {
        public string PathSegment
        {
            get { return "testapi"; }
        }

        public IEnumerable<QueryParameter> QueryParmeters
        {
            get { return new RequestParameterOptions().ToQueryParameters(); }
        }
    }

    public class StringRequest : IQuandlRequest
    {
        private readonly string _pathSegment;
        public StringRequest(string pathSegment)
        {
            _pathSegment = pathSegment;
        }
        public IContainUri Uri
        {
            get
            {
                return new StringRequestUri(_pathSegment);
            }
        }


    }

    public class StringRequestUri : IContainUri
    {
        private readonly string _pathsegment;

        public StringRequestUri(string pathsegment)
        {
            _pathsegment = pathsegment;
        }

        public string PathSegment
        {
            get { return _pathsegment; }
        }

        public IEnumerable<QueryParameter> QueryParmeters
        {
            get
            {
                return new RequestParameterOptions
                {
                    ExcludeHeaders = Exclude.True
                }.ToQueryParameters();
            }
        }
    }

    

    public class TestRequest2 : IQuandlRequest
    {
        public IContainUri Uri
        {
            get { return new TestRequest2Uri(); }
        }
    }

    public class TestRequest2Uri : IContainUri
    {
        public string PathSegment
        {
            get { return "testapi2"; }
        }

        public IEnumerable<QueryParameter> QueryParmeters
        {
            get { return new RequestParameterOptions().ToQueryParameters(); }
        }
    }

    public class PrintStatus
    {
        public void Print()
        {
            var lastRequestCount = 0;
            while (true)
            {
                var currentRequestCount = NQueue.GetQueueStatus().RequestsProcessed;
                if (NQueue.GetQueueStatus().RequestsRemaining == 0) break;
                if (currentRequestCount == lastRequestCount) continue;

                Console.Clear();
                Verbose.PrintStatus();
                lastRequestCount = currentRequestCount;
            }
          
        }
    }

    public class QuandlQueueTest
    {
        public async Task<int> Get()
        {
            var options = new RequestParameterOptions
            {
                ApiKey = "asdfasdfa"
            };

            var requests = new List<QueueRequest<FRED_GDP>>();

            for (var i = 0; i < 20; i++)
            {
                requests.Add(new QueueRequest<FRED_GDP>
                {
                    Options = options
                });
            }
            await NQueue.GetAsync(requests);
            return await Task.FromResult(0);
        }

        public async Task<int> GetTestString()
        {
            var requests = new List<TestRequest1>();
            for (var i = 0; i < 2000; i++)
            {
                requests.Add(new TestRequest1());
            }

            var responses = new List<string>();
            var tasks = requests.Select(NQueue.GetStringAsync).ToList();
            await Task.WhenAll(tasks);

            return await Task.FromResult(0);
        }

        public async Task<int> GetTest2String()
        {
            var requests = new List<TestRequest2>();
            for (var i = 0; i < 200; i++)
            {
                requests.Add(new TestRequest2());
            }

            var responses = new List<string>();
            var tasks = requests.Select(NQueue.GetStringAsync).ToList();
            await Task.WhenAll(tasks);

            return await Task.FromResult(0);
        }
    }

    public class GetQuandl
    {
        public void Get()
        {
            var options = new RequestParameterOptions();

            var service = new QuandlJsonService("https://quandl.com/api");
            var result = service.GetAsync<FRED_GDP>(options).Result;

            foreach (var entity in result.Entities)
            {
                Console.WriteLine(entity.Date + " | " + entity.Value);
            }
        }
    }

    //public class QuandlRealDeal
    //{
    //    public async Task<IEnumerable<RequestStringResponse>> GetStringResponse()
    //    {
    //        var quandlCode = new QuandlCode { DatabaseCode = "WSJ", TableCode = "MILK" };
    //        var request1 = new RequestString(quandlCode,

    //            new OptionalRequestParameters
    //            {
    //                SortOrder = SortOrder.Ascending,
    //                Column = 3,
    //                DateRange = new DateRange
    //                {
    //                    TrimStart = DateTime.Now,
    //                    TrimEnd = DateTime.Today.AddDays(-20)
    //                },
    //                ExcludeData = Exclude.False,
    //                ExcludeHeaders = Exclude.False,
    //                Rows = 6,
    //                Transformation = Transformation.CumulativeSum
    //            });


    //        var request2 = new RequestString(quandlCode,

    //            new OptionalRequestParameters
    //            {
    //                SortOrder = SortOrder.Ascending,
    //                Column = 3,
    //                DateRange = new DateRange
    //                {
    //                    TrimStart = DateTime.Now,
    //                    TrimEnd = DateTime.Today.AddDays(-20)
    //                },
    //                ExcludeData = Exclude.False,
    //                ExcludeHeaders = Exclude.False,
    //                Rows = 6,
    //                Transformation = Transformation.CumulativeSum
    //            });
    //        var requestList = new List<RequestString> { request1, request2 };
    //        var responses = await NQueue.SendRequests(requestList);
    //        return await Task.FromResult(responses);
    //    }
    //}

    public static class Verbose
    {
        public static void PrintStatus()
        {
            Console.WriteLine("                     Total: {0}", NQueue.GetQueueStatus().TotalRequests);
            Console.WriteLine("                 Remaining: {0}", NQueue.GetQueueStatus().RequestsRemaining);
            Console.WriteLine("                 Processed: {0}", NQueue.GetQueueStatus().RequestsProcessed);
            Console.WriteLine("              Time Elapsed: {0}", NQueue.GetQueueStatus().TimeElapsed);
            Console.WriteLine("            Time Remaining: {0}", NQueue.GetQueueStatus().TimeRemaining);
            Console.WriteLine("            RequestsPerSec: {0} ",NQueue.GetQueueStatus().RequestsPerSecond);
            Console.WriteLine("10 Minutes at Current Rate: {0} ",NQueue.GetQueueStatus().HowManyRequestsIn10MinutesAtCurrentRate);
            Console.WriteLine(NQueue.GetQueueStatus().LastResponse);
            //Console.WriteLine("{0}",new string('.', NQueue.GetQueueStatus().RequestsRemaining));
        }
    }

    //}


    //public class GetResults
    //{
    //    private const int RequestCount = 1000;

    //    public async Task<int> GetAllResult()
    //    {
    //        var task = Task.WhenAll(Get1(), Get2());
    //        await task;
    //        return await Task.FromResult(0);
    //    }


    //    public async Task<int> Get1()
    //    {
    //        var requests = new List<TestRequest>();
    //        for (var i = 1; i <= RequestCount; i++)
    //        {
    //            requests.Add(new TestRequest());
    //        }

    //        var actionDelegate = new Actions();

    //        var responses = await NQueue.SendRequests(requests);
    //        //  actionDelegate.ActionDelegate(responses);
    //        return await Task.FromResult(0);
    //    }

    //    public async Task<int> Get2()
    //    {
    //        var requests = new List<TestRequest2>();
    //        for (var i = 1; i <= RequestCount; i++)
    //        {
    //            requests.Add(new TestRequest2());
    //        }

    //        var actionDelegate = new Actions();

    //        var responses = await NQueue.SendRequests(requests);
    //        //    actionDelegate.ActionDelegate(responses);
    //        return await Task.FromResult(0);
    //    }
    //}


    //public class Actions
    //{
    //    public void ActionDelegate(IEnumerable<TestResponse> responses)
    //    {
    //        foreach (var response in responses)
    //        {
    //            Console.Write("Request[1] Count:" + response.RequestCount + " Request[1]: " +
    //                  response.RequestType + " | Second: " + response.Second + " | " +
    //                  response.UniqueId + " | " + response.Milliseconds);
    //            Console.WriteLine();
    //        }


    //    }

    //    public void ActionDelegate(IEnumerable<TestResponse2> responses)
    //    {
    //        foreach (var response in responses)
    //        {
    //            Console.Write("Request[2] Count:" + response.RequestCount + " Request[2]: " +
    //                    response.RequestType + " | Second: " + response.Second + " | " +
    //                    response.UniqueId2 + " | " + response.Milliseconds);
    //            Console.WriteLine();
    //        }


    //    }

    //    public void QueueDelegate(QueueStatus queueStatus)
    //    {
    //        Console.WriteLine("Requests Remaining: " + queueStatus.RequestsRemaining + " Requests Processed: " + queueStatus.RequestsProcessed);
    //    }
    //}
}