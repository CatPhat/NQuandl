using System;
using NQuandl.Client.Api;
using NQuandl.Client.CompositionRoot;
using NQuandl.Client.Domain;
using NQuandl.Client.Domain.Entities;
using NQuandl.Client.Domain.Queries;

namespace NQuandl.TestConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Bootstrapper.Bootstrap("wRFGnVkQzswRFPKq3yt_");
            var queries = (IProcessQueries) Bootstrapper.GetInstance(typeof (IProcessQueries));
            var result = queries.Execute(new DatasetBy<FredGdp>()).Result;
            foreach (var fredGdp in result.Entities)
            {
                Console.WriteLine(fredGdp.Value);
            }

            Console.WriteLine("done");
            Console.ReadLine();
        }
    }

    //public class GetV1
    //{
    //    public JsonResponseV1<FredGdp> GetFredGdp()
    //    {


    //        var result = queries.Execute(new RequestJsonResponseV1ByEntity<FredGdp>(new RequestParametersV1
    //        {
    //            ApiKey = "XXXXXX"
    //        })).Result;


    //        foreach (var fredGdp in result.Entities)
    //        {
    //            Console.WriteLine("Date: {0} | Value: {1}", fredGdp.Date, fredGdp.Value);
    //        }

    //        return result;
    //    }
    //}

    //public class GetV2
    //{
    //    public JsonResponseV2 GetJsonResponseV2()
    //    {
    //        var queries = CompositionRoot.GetQueryProcessor();
    //        var result = queries.Execute(new RequestJsonResponseV2(new RequestParametersV2
    //        {
    //            ApiKey = QuandlServiceConfiguration.ApiKey,
    //            Query = "*",
    //            SourceCode = "UNDATA",
    //            PerPage = 300,
    //            Page = 1
    //        })).Result;


    //        for (var i = 1; i <= 2000; i++)
    //        {
    //            var options = new RequestParametersV2
    //            {
    //                ApiKey = QuandlServiceConfiguration.ApiKey,
    //                Query = "*",
    //                SourceCode = "UNDATA",
    //                PerPage = 300,
    //                Page = i
    //            };

    //            requests.Add(options);
    //        }


    //        return result;
    //    }
    //}
}