using System.IO;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using NQuandl.Domain.Quandl.Responses;

namespace NQuandl.Api.Quandl.Helpers
{
    public static class ResponseExtensions
    {
        public static TResult DeserializeToEntity<TResult>(this
            Stream stream)
            where TResult : ResultWithQuandlResponseInfo
        {
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                var result = serializer.Deserialize<TResult>(jsonTextReader);
                return result;
            }
        }

        public static QuandlClientResponseInfo GetResponseInfo(this HttpClientResponse response)
        {
         
            const string rateLimitKey = "X-RateLimit-Limit";
            const string rateLimitRemainingKey = "X-RateLimit-Remaining";

            int? rateLimit = null;
            if (response.ResponseHeaders.ContainsKey(rateLimitKey))
            {
                var rateLimitString = response.ResponseHeaders.FirstOrDefault(x => x.Key == rateLimitKey).Value.FirstOrDefault();
                int temp;
                int.TryParse(rateLimitString, out temp);
                rateLimit = temp;
            }

            int? rateLimitRemaining = null;
            if (response.ResponseHeaders.ContainsKey(rateLimitRemainingKey))
            {
                var rateLimitString = response.ResponseHeaders.FirstOrDefault(x => x.Key == rateLimitRemainingKey).Value.FirstOrDefault();
                int temp;
                int.TryParse(rateLimitString, out temp);
                rateLimitRemaining = temp;
            }


            return new QuandlClientResponseInfo
            {
                IsStatusSuccessCode = response.IsStatusSuccessCode,
                StatusCode = response.StatusCode,
                ResponseHeaders = response.ResponseHeaders,
                RateLimit = rateLimit,
                RateLimitRemaining = rateLimitRemaining
            };
        }
    }
}