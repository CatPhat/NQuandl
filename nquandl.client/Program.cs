﻿using System;
using System.Linq;
using NQuandl.Client.Api;
using NQuandl.Client.Api.Helpers;
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

            var requestParameters = new RequestParametersV2
            {
                ApiKey = "71RqY1iB-mPYtst-k4vV",
                Query = "*",
                SourceCode = "UN",
                PerPage = 300,
                Page = 1,

            };

            var factory = CompositionRoot.Bootstrap();
            var queries = (IProcessQueries) factory.GetService(typeof (IProcessQueries));
            var result = queries.Execute(new RequestString(requestParameters)).Result;
            
            Console.WriteLine(result);
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