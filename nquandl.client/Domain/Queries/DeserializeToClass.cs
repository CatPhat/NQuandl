using System;
using Newtonsoft.Json;
using NQuandl.Client.Api;

namespace NQuandl.Client.Domain.Queries
{
    public class DeserializeToClass<TClass> : IDefineQuery<TClass> where TClass : class
    {
        public DeserializeToClass(string rawResponse)
        {
            RawResponse = rawResponse;
        }

        public string RawResponse { get; private set; }
    }

    public class HandleDeserializeToClass<TClass> : IHandleQuery<DeserializeToClass<TClass>, TClass>
        where TClass : class
    {
        public TClass Handle(DeserializeToClass<TClass> query)
        {
            if (query == null) throw new ArgumentNullException("query");
            return JsonConvert.DeserializeObject<TClass>(query.RawResponse);
        }
    }
}