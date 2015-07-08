using System;
using System.Linq;
using NQuandl.Client.CompositionRoot;
using NQuandl.Client.Domain.Entities;
using NQuandl.Client.Domain.Queries;
using NQuandl.Client.Domain.RequestParameters;
using NQuandl.Client.Domain.Responses;

namespace NQuandl.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
           
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }

    public class GetV1
    {
        public JsonResponseV1<FredGdp> GetFredGdp()
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

            return result;
        } 
    }

    public class GetV2
    {
        public JsonResponseV2 GetJsonResponseV2()
        {
            var queries = Bootstapper.GetQueryProcessor();
            var result = queries.Execute(new RequestJsonResponseV2(new RequestParametersV2
            {
                ApiKey = QuandlServiceConfiguration.ApiKey,
                Query = "*",
                SourceCode = "UNDATA",
                PerPage = 300,
                Page = i
            })).Result;


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

         

            return result;
        }
    }
}