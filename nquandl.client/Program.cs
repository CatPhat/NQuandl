using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Client;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;
using NQuandl.Client.Responses;
using NQuandl.Client.URIs;
using NQuandl.Queue;
using NQuandl.TestConsole;

namespace NQuandl.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var test = new TestClient();
            test.Run();

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }

    public class TestClient
    {
        public void Run()
        {
            var task = Task.WhenAll(Consume());
            while (task.IsCompleted == false)
            {
            }
        }

        public async Task<int> Consume()
        {
            var requests = new List<TesteMetadataRequestV2>();
            for (var i = 1; i <= 100; i++)
            {
                for (var j = 1; j <= 200; j++)
                {
                    var options = new ForcedRequestOptionsV2(QuandlServiceConfiguration.ApiKey, "*", "UNDATA", 300, i);
                    var request = new TesteMetadataRequestV2(options);

                    requests.Add(request);
                }
            }

            var tasks =
                requests.Select(async request =>
                {
                    var response = await NQueue.GetStringAsync(request);
                    var desrialized = new DeserializedJsonResponse<TestJsonResponseV2>(response);

                    Console.Clear();
                    Verbose.PrintStatus();
                    Verbose.PrintTestResponse(desrialized.JsonResponse);
                    Console.WriteLine();
                }).ToList();

            await Task.WhenAll(tasks);

            return await Task.FromResult(0);
        }
    }


    public class TesteMetadataRequestV2 : IQuandlJsonRequest<TestJsonResponseV2>
    {
        public readonly ForcedRequestOptionsV2 _options;

        public TesteMetadataRequestV2(ForcedRequestOptionsV2 options)
        {
            _options = options;
        }

        public IContainUri Uri
        {
            get { return new QuandlJsonUriV2(_options); }
        }
    }

    public class TestJsonResponseV2 : JsonResponse
    {
        public JsonResponseV2 MetadataResponse { get; set; }
        public Cacheresponse CacheResponse { get; set; }
    }


    public class Cacheresponse
    {
        public DateTime FirstRequestTime { get; set; }
        public DateTime LastRequestTime { get; set; }
        public string TimeElapsed { get; set; }
        public string AverageTimeBetweenRequests { get; set; }
        public Lastentry LastEntry { get; set; }
        public int RequestCount { get; set; }
        public float RequestsPerSecond { get; set; }
        public float AverageRequestsPerSecond { get; set; }
        public string TimeElapsedSinceFirstRequest { get; set; }
        public float RequestsPerLastTenMinutes { get; set; }
        public float EstimatedRequestsPer10MinutesAtCurrentRate { get; set; }
        public float EstimatedAverageRequestsPer10MinutesAtCurrentRate { get; set; }
        public int RequestsRemaining { get; set; }
    }
}

public class Lastentry
{
    public string CurrentAverageTimeBetweenRequests { get; set; }
    public DateTime LastRequestTime { get; set; }
    public int RequestId { get; set; }
    public DateTime RequestTime { get; set; }
    public string TimeSinceLastRequest { get; set; }
}


public class SaveToFile
{
    public void Run()
    {
        var printStatus = new PrintStatus();
        var task = Task.WhenAll(QueryAndSave());
        while (task.IsCompleted == false)
        {
            printStatus.Print();
        }
    }

    public async Task<int> QueryAndSave()
    {
        var requests = new List<DeserializeMetadataRequestV2>();
        for (var i = 1; i <= 6752; i++)
        {
            var options = new ForcedRequestOptionsV2(QuandlServiceConfiguration.ApiKey, "*", "UN", 300, i);
            var request = new DeserializeMetadataRequestV2(options);
            requests.Add(request);
        }

        var tasks = requests.Select(async request =>
        {
            var response = await NQueue.GetStringAsync(request);
            using (var writer =
                new StreamWriter(@"A:\DEVOPS\NQuandl\NQuandl.Generator\testresponses\" +
                                 request._options.SourceCode + request._options.Page + ".json"))
            {
                writer.Write(response);
            }
        }).ToList();

        await Task.WhenAll(tasks);
        return await Task.FromResult(0);
    }
}


public class QueueTest
{
    public void Run()
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
    }
}

