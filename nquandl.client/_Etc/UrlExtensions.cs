using System;


namespace NQuandl.Client
{
    public static class UrlExtensions
    {
        public static string AppendApiKey(this string uri, string apiKey)
        {
            return uri + "&auth_token=" + apiKey;
        }

        internal static RequestAttributes GetRequestAttributes<T>(this BaseQuandlRequestWithQueryOptions<T> request) where T : QuandlResponse
        {
            var attributes = (RequestAttributes[]) typeof (T).GetCustomAttributes(typeof (RequestAttributes), true);
            if (attributes.Length <= 0) return null;
            var requestAttributes = new RequestAttributes
            {
                DatabaseCode = attributes[0].DatabaseCode,
                TableCode = attributes[1].TableCode
            };
            return requestAttributes;
        }

    }
}


