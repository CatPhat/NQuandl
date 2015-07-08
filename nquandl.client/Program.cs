using System;
using NQuandl.Client.CompositionRoot;
using NQuandl.Client.Domain.Queries;
using NQuandl.Client.Domain.RequestParameters;

namespace NQuandl.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var queries = Bootstapper.GetQueryProcessor();
            var result = queries.Execute(new RequestJsonFredGdp
            {
                RequestParametersV1 = new RequestParametersV1
                {
                    ApiKey = "XXXXXX"
                }
            }).Result;

            Console.WriteLine(result.Data);
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}