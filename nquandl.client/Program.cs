using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Client;
using NQuandl.Client.Helpers;
using NQuandl.Client.Interfaces;
using NQuandl.Client.Requests;
using NQuandl.Client.Responses;
using NQuandl.Client.URIs;
using NQuandl.Queue;
using Rebus;
using Rebus.Configuration;
using Rebus.Logging;
using Rebus.Messages;
using Rebus.Persistence.SqlServer;
using Rebus.Transports.Sql;

namespace NQuandl.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var test = new TestBus();
            test.Run();

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }

    public class TestBus
    {
        public void Run()
        {
            using (var adapter = new BuiltinContainerAdapter())
            {
                adapter.Register(() => new HandleQueueRequest());
                adapter.Register(() => new PrintDateTime());
                var bus = Configure.With(adapter)
                    .Logging(l => l.ColoredConsole(LogLevel.Error))
                    .Transport(
                        t =>
                            t.UseSqlServer(@"server=SHIVA9.;initial catalog=RebusInputQueue;integrated security=sspi",
                                "thequeue", "my-app.input", "my-app.error").EnsureTableIsCreated())
                    .Timeouts(
                        x =>
                            x.Use(
                                new SqlServerTimeoutStorage(
                                    @"server=SHIVA9.;initial catalog=RebusInputQueue;integrated security=sspi",
                                    "timeouts").EnsureTableIsCreated()))
                    .MessageOwnership(x => x.Use(new DetermineQueueOwnership()))
                    .CreateBus()
                    .Start();

                var requests = new List<QueryParametersV2>();
                for (var i = 1; i <= 10; i++)
                {
                    for (var j = 1; j <= 2000; j++)
                    {
                        var options = new QueryParametersV2
                        {
                            ApiKey = QuandlServiceConfiguration.ApiKey,
                            Query = "*",
                            SourceCode = "UNDATA",
                            PerPage = 300,
                            Page = i
                        };

                        requests.Add(options);
                    }
                }


                foreach (var request in requests)
                {
                    bus.Defer(TimeSpan.FromMilliseconds(300*requests.IndexOf(request)), request);
                }
                Console.WriteLine("Press enter to quit");
                Console.ReadLine();
            }
        }

        public class DetermineQueueOwnership : IDetermineMessageOwnership
        {
            public string GetEndpointFor(Type messageType)
            {
                if (messageType == typeof (DeserializeMetadataRequestV2))
                {
                    return "my-app.input";
                }
                throw new Exception("no endpoint for message type");
            }
        }

        public class HandleQueueRequest : IHandleMessages<QueryParametersV2>
        {
            private readonly IQuandlJsonService _client;

            public HandleQueueRequest()
            {
                _client = new QuandlJsonService(QuandlServiceConfiguration.BaseUrl);
            }

            public async void Handle(QueryParametersV2 message)
            {
                var request = new TestMetadataRequestV2(message);
                var response = await _client.GetAsync(request);
                Verbose.PrintTestResponse(response.JsonResponse);
            }
        }

        private class PrintDateTime : IHandleMessages<DateTime>
        {
            public void Handle(DateTime currentDateTime)
            {
                Console.WriteLine("The time is {0}", currentDateTime);
            }
        }
    }


    public class QueueRequestSerializer : ISerializeMessages
    {
        public TransportMessageToSend Serialize(Message message)
        {
            var thisMessage = message;
            var transportMessage = new TransportMessageToSend();

            throw new NotImplementedException();
        }

        public Message Deserialize(ReceivedTransportMessage transportMessage)
        {
            throw new NotImplementedException();
        }
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
        public float RequestsSinceFirstRequestTime { get; set; }
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
            var requests = new List<TestMetadataRequestV2>();
            for (var i = 1; i <= 100; i++)
            {
                for (var j = 1; j <= 200; j++)
                {
                    var options = new QueryParametersV2
                    {
                        ApiKey = QuandlServiceConfiguration.ApiKey,
                        Query = "*",
                        SourceCode = "UNDATA",
                        PerPage = 300,
                        Page = i
                    };

                    var request = new TestMetadataRequestV2(options);

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


    public class TestJsonResponseV2 : JsonResponse
    {
        public JsonResponseV2 MetadataResponse { get; set; }
        public Cacheresponse CacheResponse { get; set; }
    }

    public class TestMetadataRequestV2 : IQuandlJsonRequest<TestJsonResponseV2>
    {
        public readonly QueryParametersV2 _options;

        public TestMetadataRequestV2(QueryParametersV2 options)
        {
            _options = options;
        }

        public IQuandlUri Uri
        {
            get { return new QuandlUriV2(ResponseFormat.JSON, _options); }
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
                var options = new QueryParametersV2
                {
                    ApiKey = QuandlServiceConfiguration.ApiKey,
                    Query = "*",
                    SourceCode = "UNDATA",
                    PerPage = 300,
                    Page = i
                };
                var request = new DeserializeMetadataRequestV2(options);
                requests.Add(request);
            }

            //var tasks = requests.Select(async request =>
            //{
            //    var response = await NQueue.GetStringAsync(request);
            //    using (var writer =
            //        new StreamWriter(@"A:\DEVOPS\NQuandl\NQuandl.Generator\testresponses\" +
            //                         request._options.SourceCode + request._options.Page + ".json"))
            //    {
            //        writer.Write(response);
            //    }
            //}).ToList();

            //await Task.WhenAll(tasks);
            return await Task.FromResult(0);
        }
    }
}