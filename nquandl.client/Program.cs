using System;
using System.Threading.Tasks;
using NQuandl.Client;
using NQuandl.Client.Helpers;
using NQuandl.Client.Requests;

namespace NQuandl.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = new RequestParameterOptions
            {
                ApiKey = QuandlServiceConfiguration.ApiKey,
                ExcludeData = Exclude.True

            };
            var client = new QuandlService("https://quandl.com/api");
            var request = new JsonStringRequest("FRED/GDP")
            {
                Options = options
            };

            var result = client.GetStringAsync(request).Result;
            Console.WriteLine(result);
            Console.WriteLine("done");
            Console.ReadLine();
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
}