using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NQuandl.Client.Entities;
using NQuandl.Client.Requests;
using NQuandl.Queue;

namespace NQuandl.TestConsole
{
    public class QuandlQueueTest
    {
        public async Task<int> Get()
        {
            var options = new RequestOptionsV1
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
}