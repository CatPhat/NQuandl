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

        public static QuandlClientResponseInfo GetResponseInfo(this HttpResponseMessage response)
        {
            if (response.Headers == null)
            {
                return null;
            }
            var headers = response.Headers.ToDictionary(httpHeader => httpHeader.Key, httpHeader => httpHeader.Value);

            const string rateLimitKey = "X-RateLimit-Limit";
            const string rateLimitRemainingKey = "X-RateLimit-Remaining";

            int? rateLimit = null;
            if (headers.ContainsKey(rateLimitKey))
            {
                var rateLimitString = headers.FirstOrDefault(x => x.Key == rateLimitKey).Value.FirstOrDefault();
                int temp;
                int.TryParse(rateLimitString, out temp);
                rateLimit = temp;
            }

            int? rateLimitRemaining = null;
            if (headers.ContainsKey(rateLimitRemainingKey))
            {
                var rateLimitString = headers.FirstOrDefault(x => x.Key == rateLimitRemainingKey).Value.FirstOrDefault();
                int temp;
                int.TryParse(rateLimitString, out temp);
                rateLimitRemaining = temp;
            }


            return new QuandlClientResponseInfo
            {
                IsStatusSuccessCode = response.IsSuccessStatusCode,
                StatusCode = response.StatusCode.ToString(),
                ResponseHeaders = headers,
                RateLimit = rateLimit,
                RateLimitRemaining = rateLimitRemaining
            };
        }
    }
}