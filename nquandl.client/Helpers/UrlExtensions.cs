using NQuandl.Client.Requests;

namespace NQuandl.Client.Helpers
{
    public static class UrlExtensions
    {
        public static string ToV1Url(this RequestParameters parameters)
        {
            var url = parameters.BaseUrl +
                    "/" + parameters.ApiVersion +
                    "/" + parameters.QuandlCode +
                    "." + parameters.RequestFormat +
                    "?" + parameters.ApiKey;

            if (parameters.Options == null)
            {
                return url;
            }
            return url + "&" + parameters.Options.ToRequestParameter();
        }
    }
}
