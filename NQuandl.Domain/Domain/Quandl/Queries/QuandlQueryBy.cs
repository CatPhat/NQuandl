//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Flurl.Util;
//using Newtonsoft.Json;
//using NQuandl.Api.Quandl;
//using NQuandl.Api.Transactions;
//using NQuandl.Domain.Quandl.RequestParameters;
//using NQuandl.Domain.Quandl.Responses;

//namespace NQuandl.Domain.Quandl.Queries
//{
//    public class QuandlQueryBy<TResult> : IDefineQuery<Task<TResult>> where TResult : ResultWithQuandlResponseInfo
//    {
//        public QuandlQueryBy(QuandlClientRequestParameters requestParameters)
//        {
//            RequestParameters = requestParameters;
//        }

//        public QuandlClientRequestParameters RequestParameters { get; }
//    }

//    public class HandleQuandlQueryBy<TResult> : IHandleQuery<QuandlQueryBy<TResult>, Task<TResult>>
//        where TResult : ResultWithQuandlResponseInfo
//    {
//        private readonly IQuandlClient _client;

//        public HandleQuandlQueryBy(IQuandlClient client)
//        {
//            if (client == null) throw new ArgumentNullException(nameof(client));

//            _client = client;
//        }

//        public async Task<TResult> Handle(QuandlQueryBy<TResult> query)
//        {
//            var response = await _client.GetAsync(query.RequestParameters);
//            var serializer = new JsonSerializer();
//            using (var sr = new StreamReader(response.ContentStream))
//            using (var jsonTextReader = new JsonTextReader(sr))
//            {
//                var result = serializer.Deserialize<TResult>(jsonTextReader);
              

//                return result;
//            }
//        }
//    }
//}