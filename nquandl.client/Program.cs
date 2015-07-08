using System;
using System.Linq;
using NQuandl.Client.CompositionRoot;
using NQuandl.Client.Domain.Entities;
using NQuandl.Client.Domain.Queries;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var queries = Bootstapper.GetQueryProcessor();
            var result = queries.Execute(new RequestJsonResponseV1ByEntity<FredGdp>(new RequestParametersV1
            {
                ApiKey = "XXXXXX"
            })).Result;


            foreach (var fredGdp in result.Entities)
            {
                Console.WriteLine("Date: {0} | Value: {1}", fredGdp.Date, fredGdp.Value);
            }
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}