using System.IO;
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

        public static TResult DeserializeToEntity<TResult>(this string responseString)
        {
            return JsonConvert.DeserializeObject<TResult>(responseString);
        }

        public static QuandlClientResponseInfo GetResponseInfo(this HttpClientResponse response)
        {
            var info = new QuandlClientResponseInfo
            {
                IsStatusSuccessCode = response.IsStatusSuccessCode,
                StatusCode = response.StatusCode,
                ResponseHeaders = response.ResponseHeaders,
                QuandlErrorResponse = new QuandlErrorResponse()
            };

            if (response.IsStatusSuccessCode) return info;


            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(response.ContentStream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                info.QuandlErrorResponse = serializer.Deserialize<QuandlErrorResponse>(jsonTextReader);
            }

            return info;
        }
    }
}