using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Client;
using NQuandl.Client.Requests;
using NQuandl.Queue;

namespace NQuandl.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var test = new SaveToFile();
            test.Run();

            Console.WriteLine("done");
            Console.ReadLine();
        }
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
            for (var i = 2256; i <= 2451; i++)
            {
                var options = new ForcedRequestOptionsV2(QuandlServiceConfiguration.ApiKey, "*", "WORLDBANK", 300, i);
                var request = new DeserializeMetadataRequestV2(options);
                requests.Add(request);
            }

            var tasks = requests.Select(async request =>
            {
                var response = await NQueue.GetStringAsync(request);
                using (                    var writer =
                        new StreamWriter(@"C:\Users\kian.monsoon\Documents\SYSADMIN\DEV\GITHUB\NQuandl\responses\" +
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
}