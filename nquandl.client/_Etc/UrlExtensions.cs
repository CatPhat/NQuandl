using System;


namespace NQuandl.Client
{
    public static class UrlExtensions
    {
        public static string AppendApiKey(this string uri, string apiKey)
        {
            return uri + "&auth_token=" + apiKey;
        }

    }
}


